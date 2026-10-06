using BroStreamerTools;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("debugthis", out bool debug);
        return AhkClient.Send("ActivateStreamFeedApp", debug);
    }
}
