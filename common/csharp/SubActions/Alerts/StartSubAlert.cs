using System;
using BroStreamerTools.Logging;

public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("userName", out string user);
        CPH.TryGetArg("messageStripped", out string message);
        CPH.TryGetArg("tier", out string tier);
        CPH.TryGetArg("cumulative", out string cumulative); // only present on ReSub

        bool isResub = !string.IsNullOrEmpty(cumulative);

        message ??= "";
        tier ??= "1";
        cumulative ??= "0";

        string json =
            "{"
            + "\"event\":\"subAlert\","
            + "\"user\":\""
            + Escape(user)
            + "\","
            + "\"tier\":\""
            + Escape(tier)
            + "\","
            + "\"message\":\""
            + Escape(message)
            + "\","
            + "\"isResub\":"
            + (isResub ? "true" : "false")
            + ","
            + "\"months\":\""
            + Escape(cumulative)
            + "\""
            + "}";

        BroLogger.Info($"Broadcasting JSON: {json}");

        CPH.WebsocketBroadcastJson(json);
        return true;
    }

    private string Escape(string s) => (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"");
}
