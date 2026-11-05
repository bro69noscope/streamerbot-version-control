using BroStreamerTools;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.ObsSendRaw(
            "SetInputVolume",
            "{\"inputName\":\"audio__mixer_usb_out\",\"inputVolumeDb\":0}",
            ObsConn.Prod
        );
        return true;
    }
}
