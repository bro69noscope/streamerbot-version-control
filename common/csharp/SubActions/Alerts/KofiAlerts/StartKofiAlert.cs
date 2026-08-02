using System;
using BroStreamerTools.Logging;

public class CPHInline
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
            BroLogger.Info($"__source: found={source != null}, value='{source}'");
            BroLogger.Info($"amount: found={amount != null}, value='{amount}'");
            BroLogger.Info($"currency: found={currency != null}, value='{currency}'");
            BroLogger.Info($"from: found={from != null}, value='{from}'");
            BroLogger.Info($"message: found={message != null}, value='{message}'");
        }

        string kind;
        if (source == "KofiSubscription")
        {
            kind = "kofisub";
        }
        else if (source == "KofiResubscription")
        {
            kind = "kofiresub";
        }
        else if (source == "KofiDonation")
        {
            kind = "kofitip";
        }
        else
        {
            kind = "unknown";
        }

        amount ??= "0";
        currency ??= "USD";
        message ??= "";
        if (string.IsNullOrEmpty(from))
            from = "Someone anonymous";

        string json =
            "{"
            + "\"event\":\"KofiAlert\","
            + "\"kind\":\""
            + kind
            + "\","
            + "\"user\":\""
            + Escape(from)
            + "\","
            + "\"amount\":\""
            + Escape(amount)
            + "\","
            + "\"currency\":\""
            + Escape(currency)
            + "\","
            + "\"message\":\""
            + Escape(message)
            + "\""
            + "}";

        if (debugEnabled)
        {
            BroLogger.Info($"Broadcasting source={source}, kind={kind}, json={json}");
        }

        CPH.WebsocketBroadcastJson(json);
        return true;
    }

    private string Escape(string s) => (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"");
}
