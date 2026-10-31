using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("sbArgDebugThis", out bool debugEnabled);
        var payload = new { @event = "SkipCurrentAlert" };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        CPH.WebsocketBroadcastJson(json);

        if (debugEnabled)
            BroLogger.Debug($"[SkipCurrentAlert] dispatched: {json}");

        return true;
    }
}
