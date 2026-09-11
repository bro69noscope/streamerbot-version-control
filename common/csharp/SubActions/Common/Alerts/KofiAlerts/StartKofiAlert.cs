using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("__source", out string source);
        CPH.TryGetArg("amount", out string amount);
        CPH.TryGetArg("currency", out string currency);
        CPH.TryGetArg("from", out string from);
        CPH.TryGetArg("message", out string message);
        CPH.TryGetArg("debugthis", out bool debugEnabled);

        if (debugEnabled)
        {
            BroLogger.Debug($"__source: found={source != null}, value='{source}'");
            BroLogger.Debug($"amount: found={amount != null}, value='{amount}'");
            BroLogger.Debug($"currency: found={currency != null}, value='{currency}'");
            BroLogger.Debug($"from: found={from != null}, value='{from}'");
            BroLogger.Debug($"message: found={message != null}, value='{message}'");
        }

        string kind;
        switch (source)
        {
            case "KofiSubscription":
                kind = "kofisub";
                break;
            case "KofiResubscription":
                kind = "kofiresub";
                break;
            case "KofiDonation":
                kind = "kofitip";
                break;
            default:
                kind = "unknown";
                if (debugEnabled)
                    BroLogger.Info(
                        $"Unrecognized __source '{source}', falling back to kind=unknown"
                    );
                break;
        }

        amount ??= "0";
        currency ??= "USD";
        message ??= "";
        if (string.IsNullOrEmpty(from))
            from = "Someone anonymous";

        var payload = new
        {
            @event = "KofiAlert",
            kind,
            user = from,
            amount,
            currency,
            message,
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
