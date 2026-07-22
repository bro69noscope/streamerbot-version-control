using System;
using BroStreamerTools.Logging;

namespace BroStreamerTools;

public static class BRBManager
{
    public static void Start(object cph, string duration)
    {
        BroLogger.Info($"BRB started: {duration} minutes");

        Invoke(cph, "SetGlobalVar", "BRBDuration", duration, true);
        Invoke(cph, "SetGlobalVar", "BRBStartTime", DateTime.Now, true);

        string scene = (string)Invoke(cph, "ObsGetCurrentScene", 0);

        if (scene != "scene_lounge")
        {
            Invoke(cph, "SetGlobalVar", "SkipAutoHide", true, true);
        }
    }

    private static object Invoke(object cph, string name, params object[] args)
    {
        var argTypes = Array.ConvertAll(args, a => a.GetType());

        var method = cph.GetType().GetMethod(name, argTypes);

        return method == null
            ? throw new MissingMethodException(
                $"Could not find {name}({string.Join(", ", Array.ConvertAll(argTypes, t => t.Name))})"
            )
            : method.Invoke(cph, args);
    }
}
