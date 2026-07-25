using BroStreamerTools;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("brbDuration", out string brbDuration);
        CPH.LogInfo(typeof(BRBManager).Assembly.FullName);
        BRBManager.Start(CPH, brbDuration);
        return true;
    }
}
