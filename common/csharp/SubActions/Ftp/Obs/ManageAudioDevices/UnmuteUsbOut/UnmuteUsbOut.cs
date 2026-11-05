using BroStreamerTools.Obs;

public partial class CPHInline
{
    public bool Execute()
    {
        string scene = CPH.ObsGetCurrentScene();
        string source = "audio__mixer_usb_out";
        int obsConnection = ObsConn.Default;

        CPH.ObsSourceUnMute(scene, source, obsConnection);

        return true;
    }
}
