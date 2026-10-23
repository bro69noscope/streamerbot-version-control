using BroStreamerTools.Logging;

public partial class CPHInline
{
    const string InputName = "capture__window__dynamic_hwnd";
    const int ObsConnection = 0;

    public bool Execute()
    {
        string r = CPH.ObsSendRaw(
            "GetInputSettings",
            "{\"inputName\":\"" + InputName + "\"}",
            ObsConnection
        );
        BroLogger.Info(r);
        return true;
    }
}
