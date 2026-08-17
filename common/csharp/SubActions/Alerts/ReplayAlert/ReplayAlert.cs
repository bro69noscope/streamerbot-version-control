using System;
using BroStreamerTools.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CPHInline
{
    public bool Execute()
    {
        if (
            !CPH.TryGetArg("replayPayloadJson", out string replayPayloadJson)
            || string.IsNullOrEmpty(replayPayloadJson)
        )
        {
            BroLogger.Error("ReplayAlert: missing/invalid replayPayloadJson arg");
            return false;
        }
        try
        {
            var parsed = JToken.Parse(replayPayloadJson);
            CPH.WebsocketBroadcastJson(parsed.ToString(Formatting.None));
            return true;
        }
        catch (Newtonsoft.Json.JsonException ex)
        {
            BroLogger.Error($"ReplayAlert: bad JSON: {ex.Message}");
            return false;
        }
    }
}
