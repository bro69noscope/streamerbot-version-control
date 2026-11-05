using BroStreamerTools.Logging;

namespace BroStreamerTools.Obs;

public static class ObsDevices
{
    public static string MainCamera => Get("{{MAIN_CAMERA_DEVICE}}");

    static string Get(string token)
    {
        if (ObsMappingValues.All.TryGetValue(token, out var value))
        {
            return value;
        }

        BroLogger.Error($"ObsDevices: '{token}' not found in the baked OBS mappings");
        return null;
    }
}
