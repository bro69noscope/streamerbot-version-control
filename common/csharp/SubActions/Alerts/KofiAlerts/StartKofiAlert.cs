using System;
using BroStreamerTools.Logging;

public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("amount", out string amount);
        CPH.TryGetArg("currency", out string currency);
        CPH.TryGetArg("from", out string from);
        CPH.TryGetArg("message", out string message);
        CPH.TryGetArg("__source", out string source);

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
            kind = "tip";
        }
        else
        {
            kind = "unknown";
        }

        amount ??= "0";
        currency ??= "USD";
        from ??= "Someone";
        message ??= "";

        string json =
            "{"
            + "\"event\":\"tipAlert\","
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

        BroLogger.Info($"Broadcasting source={source}, kind={kind}, json={json}");

        CPH.WebsocketBroadcastJson(json);
        return true;
    }

    private string Escape(string s) => (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"");
}
