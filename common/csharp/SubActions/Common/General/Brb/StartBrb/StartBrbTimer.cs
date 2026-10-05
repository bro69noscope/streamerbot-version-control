using BroStreamerTools;
using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("debugthis", out bool debugEnabled);
        CPH.TryGetArg("brbDuration", out string brbDuration);

        if (debugEnabled)
            BroLogger.Debug(typeof(BrbManager).Assembly.FullName);

        BrbManager.Start(CPH, brbDuration, debugEnabled);
        return true;
    }
}
