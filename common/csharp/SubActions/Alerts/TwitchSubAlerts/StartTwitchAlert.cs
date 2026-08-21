using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("__source", out string source);
        CPH.TryGetArg("userName", out string user);
        CPH.TryGetArg("messageStripped", out string message);
        CPH.TryGetArg("tier", out string tier);
        CPH.TryGetArg("cumulative", out string cumulative); // present on ReSub
        CPH.TryGetArg("recipientUserName", out string recipient); // present on Gift Sub
        CPH.TryGetArg("gifts", out string giftCount); // present on Gift Bomb
        CPH.TryGetArg("debugthis", out bool debugEnabled);

        if (debugEnabled)
        {
            BroLogger.Debug($"__source: found={source != null}, value='{source}'");
            BroLogger.Debug($"userName: found={user != null}, value='{user}'");
            BroLogger.Debug($"messageStripped: found={message != null}, value='{message}'");
            BroLogger.Debug($"tier: found={tier != null}, value='{tier}'");
            BroLogger.Debug($"cumulative: found={cumulative != null}, value='{cumulative}'");
            BroLogger.Debug($"recipientUserName: found={recipient != null}, value='{recipient}'");
            BroLogger.Debug($"gifts: found={giftCount != null}, value='{giftCount}'");
        }

        string kind;
        switch (source)
        {
            case "TwitchFollow":
                kind = "follow";
                break;
            case "TwitchGiftBomb":
                kind = "giftbomb";
                break;
            case "TwitchGiftSub":
                kind = "giftsub";
                break;
            case "TwitchReSub":
                kind = "resub";
                break;
            case "TwitchSub":
                kind = "sub";
                break;
            default:
                kind = "unknown";
                if (debugEnabled)
                    BroLogger.Debug(
                        $"Unrecognized __source '{source}', falling back to kind=unknown"
                    );
                break;
        }

        message ??= "";
        tier ??= "tier 1";
        cumulative ??= "0";
        recipient ??= "";
        giftCount ??= "0";
        if (string.IsNullOrEmpty(user))
            user = "Someone anonymous";

        tier = System.Text.RegularExpressions.Regex.Match(tier, @"\d+").Value;
        if (string.IsNullOrEmpty(tier))
            tier = "1";

        var payload = new
        {
            @event = "TwitchAlert",
            kind,
            user,
            tier,
            message,
            months = cumulative,
            recipient,
            giftCount,
        };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        if (debugEnabled)
        {
            BroLogger.Debug($"Broadcasting source={source}, kind={kind}, json={json}");
        }

        CPH.WebsocketBroadcastJson(json);
        return true;
    }
}
