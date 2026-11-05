using BroStreamerTools;

public partial class CPHInline
{
    public bool Execute()
    {
        string scene = CPH.ObsGetCurrentScene();
        string source = "audio__mixer_5/6";
        int obsConnection = ObsConn.Default;

        CPH.ObsSourceUnMute(scene, source, obsConnection);

        return true;
    }
}
