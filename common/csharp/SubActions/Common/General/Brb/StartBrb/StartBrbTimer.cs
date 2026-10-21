using BroStreamerTools;
using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("debugthis", out bool debugEnabled);

        if (
            !CPH.TryGetArg("brbDuration", out string brbDuration)
            || string.IsNullOrEmpty(brbDuration)
        )
        {
            BroLogger.Error("brbDuration arg missing");
            return false;
        }

        if (debugEnabled)
            BroLogger.Debug(typeof(BrbManager).Assembly.FullName);

        BrbManager.Start(CPH, brbDuration, debugEnabled);
        return true;
    }
}
