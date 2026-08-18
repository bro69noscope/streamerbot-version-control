using BroStreamerTools;
using BroStreamerTools.Logging;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("brbDuration", out string brbDuration);
        BroLogger.Info(typeof(BRBManager).Assembly.FullName);
        BRBManager.Start(CPH, brbDuration);
        return true;
    }
}
