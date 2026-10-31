using System;
using BroStreamerTools.Logging;

namespace BroStreamerTools;

public static class BrbManager
{
    public static void Start(object cph, string duration, bool debugEnabled)
    {
        BroLogger.Info($"BRB started: {duration} minutes");

        Invoke(cph, "SetGlobalVar", "sbGlobBrbDuration", duration, true);
        Invoke(cph, "SetGlobalVar", "sbGlobBrbStartTime", DateTime.Now, true);

        const string skipAutoHideVar = "sbGlobSkipBrbAutoHide";
        string scene = (string)Invoke(cph, "ObsGetCurrentScene", 0);

        if (debugEnabled)
            BroLogger.Debug($"Current scene is '{scene}'");

        if (scene != "scene__lounge")
        {
            // the `sbact__auto_hide_brb` action, triggered once we change to the
            // scene__lounge scene, will run later than the synchronous path we
            // take throughout the sub-actions. That is, even tho we only
            // change the scene at the very end of it after showing the brb
            // text source. This text would end up hidden immediately after
            // showing without a skip flag.
            Invoke(cph, "SetGlobalVar", skipAutoHideVar, true, true);

            if (debugEnabled)
                BroLogger.Debug($"setting {skipAutoHideVar} to true");
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
