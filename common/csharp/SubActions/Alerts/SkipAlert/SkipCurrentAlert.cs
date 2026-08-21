using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        var payload = new { @event = "SkipCurrentAlert" };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        CPH.WebsocketBroadcastJson(json);

        BroLogger.Info($"[SkipCurrentAlert] dispatched: {json}");

        return true;
    }
}
