using BroStreamerTools;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("sbArgDebugThis", out bool debug);
        return AhkClient.Send("ActivateStreamFeedApp", debug);
    }
}
