using BroStreamerTools;
using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("sbArgDebugThis", out bool debugEnabled);

        if (
            !CPH.TryGetArg("sbArgBrbDuration", out string brbDuration)
            || string.IsNullOrEmpty(brbDuration)
        )
        {
            BroLogger.Error("sbArgBrbDuration arg missing");
            return false;
        }

        if (debugEnabled)
            BroLogger.Debug(typeof(BrbManager).Assembly.FullName);

        BrbManager.Start(CPH, brbDuration, debugEnabled);
        return true;
    }
}
