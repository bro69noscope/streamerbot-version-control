public partial class CPHInline
{
    public bool Execute()
    {
        string json = "{\"clearStreamFeed\": true}";
        CPH.WebsocketBroadcastJson(json);
        return true;
    }
}
