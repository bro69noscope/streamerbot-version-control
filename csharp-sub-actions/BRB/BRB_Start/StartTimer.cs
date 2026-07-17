using BroStreamerTools;

public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("brbDuration", out int brbDuration);
        CPH.LogInfo(typeof(BRBManager).Assembly.FullName);
        BRBManager.Start(CPH, brbDuration);
        return true;
    }
}
