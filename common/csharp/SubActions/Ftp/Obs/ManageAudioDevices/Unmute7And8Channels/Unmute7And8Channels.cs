using BroStreamerTools.Obs;

public partial class CPHInline
{
    public bool Execute()
    {
        string scene = CPH.ObsGetCurrentScene();
        string source = "audio__mixer_7/8";
        int obsConnection = ObsConn.Default;

        CPH.ObsSourceUnMute(scene, source, obsConnection);

        return true;
    }
}
