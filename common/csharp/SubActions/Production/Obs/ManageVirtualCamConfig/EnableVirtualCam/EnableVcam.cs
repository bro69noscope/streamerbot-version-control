using BroStreamerTools;
using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    const string VirtualCamId = "OBS Virtual Camera:";

    public bool Execute()
    {
        string hardwareCamId = ObsDevices.MainCamera;
        if (hardwareCamId == null)
        {
            return false;
        }

        SetCamDevice("capture__device__facecam", VirtualCamId, ObsConn.Prod);
        SetCamDevice("capture__device__vcam_facecam", hardwareCamId, ObsConn.Vcam);
        CPH.ObsSendRaw("StartVirtualCam", "{}", ObsConn.Vcam);
        return true;
    }

    void SetCamDevice(string input, string deviceId, int conn)
    {
        CPH.ObsSendRaw(
            "SetInputSettings",
            new JObject
            {
                ["inputName"] = input,
                ["inputSettings"] = new JObject { ["video_device_id"] = deviceId },
            }.ToString(),
            conn
        );
    }
}
