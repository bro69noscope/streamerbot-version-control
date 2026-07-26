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

        BroLogger.Info($"kind={kind}, json={json}");

        CPH.WebsocketBroadcastJson(json);
        return true;
    }

    private string Escape(string s) => (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"");
}
