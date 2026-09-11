using BroStreamerTools;
using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("debugthis", out bool debugEnabled);
        CPH.TryGetArg("brbDuration", out string brbDuration);
        if (debugEnabled)
            BroLogger.Debug(typeof(BRBManager).Assembly.FullName);
        BRBManager.Start(CPH, brbDuration);
        return true;
    }
}
