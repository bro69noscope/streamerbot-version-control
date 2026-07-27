using System;
using BroStreamerTools.Logging;

public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("userName", out string user);
        CPH.TryGetArg("messageStripped", out string message);
        CPH.TryGetArg("tier", out string tier);
        CPH.TryGetArg("cumulative", out string cumulative); // present on ReSub
        CPH.TryGetArg("recipientUserName", out string recipient); // present on Gift Sub
        CPH.TryGetArg("gifts", out string giftCount); // present on Gift Bomb
        CPH.TryGetArg("debugthis", out bool debugEnabled);

        if (debugEnabled)
        {
            BroLogger.Info($"userName: found={user != null}, value='{user}'");
            BroLogger.Info($"messageStripped: found={message != null}, value='{message}'");
            BroLogger.Info($"tier: found={tier != null}, value='{tier}'");
            BroLogger.Info($"cumulative: found={cumulative != null}, value='{cumulative}'");
            BroLogger.Info($"recipientUserName: found={recipient != null}, value='{recipient}'");
            BroLogger.Info($"gifts: found={giftCount != null}, value='{giftCount}'");
        }

        string kind;
        if (!string.IsNullOrEmpty(giftCount))
        {
            kind = "giftbomb";
        }
        else if (!string.IsNullOrEmpty(recipient))
        {
            kind = "giftsub";
        }
        else if (!string.IsNullOrEmpty(cumulative))
        {
            kind = "resub";
        }
        else
        {
            kind = "sub";
        }

        message ??= "";
        tier ??= "1";
        cumulative ??= "0";
        recipient ??= "";
        giftCount ??= "0";
        user ??= "Someone";

        string json =
            "{"
            + "\"event\":\"subAlert\","
            + "\"kind\":\""
            + kind
            + "\","
            + "\"user\":\""
            + Escape(user)
            + "\","
            + "\"tier\":\""
            + Escape(tier)
            + "\","
            + "\"message\":\""
            + Escape(message)
            + "\","
            + "\"months\":\""
            + Escape(cumulative)
            + "\","
            + "\"recipient\":\""
            + Escape(recipient)
            + "\","
            + "\"giftCount\":\""
            + Escape(giftCount)
            + "\""
            + "}";

        if (debugEnabled)
        {
            BroLogger.Info($"kind={kind}, json={json}");
        }

        CPH.WebsocketBroadcastJson(json);
        return true;
    }

    private string Escape(string s) => (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"");
}
