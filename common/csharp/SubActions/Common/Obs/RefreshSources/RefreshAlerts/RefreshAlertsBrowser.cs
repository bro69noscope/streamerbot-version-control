using BroStreamerTools;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.ObsSendRaw(
            "PressInputPropertiesButton",
            "{\"inputName\":\"browser__streamerbot_alerts\",\"propertyName\":\"refreshnocache\"}",
            ObsConn.Default
        );
        return true;
    }
}
