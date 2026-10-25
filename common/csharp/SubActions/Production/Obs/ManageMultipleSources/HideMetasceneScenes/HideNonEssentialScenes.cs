// Required references to manually add (not included in our sbot dlls):
//   - System.Core.dll

using System;
using BroStreamerTools;
using BroStreamerTools.Logging;
using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    const string ParentScene = MetasceneScenes.Parent;

    public bool Execute()
    {
        CPH.TryGetArg("debugthis", out bool debugEnabled);

        string raw = CPH.ObsSendRaw(
            "GetSceneItemList",
            new JObject { ["sceneName"] = ParentScene }.ToString(),
            0
        );
        var items = JObject.Parse(raw)["sceneItems"];

        if (items == null)
        {
            BroLogger.Error($"No sceneItems for {ParentScene}. Raw: {raw}");
            return false;
        }

        foreach (var item in items)
        {
            string name = (string)item["sourceName"];
            bool keep = MetasceneScenes.IsEssential(name);

            string set = new JObject
            {
                ["sceneName"] = ParentScene,
                ["sceneItemId"] = (int)item["sceneItemId"],
                ["sceneItemEnabled"] = keep,
            }.ToString();

            try
            {
                CPH.ObsSendRaw("SetSceneItemEnabled", set, 0);
                if (debugEnabled)
                {
                    BroLogger.Debug($"{name} -> {(keep ? "shown" : "hidden")}");
                }
            }
            catch (Exception ex)
            {
                BroLogger.Error($"Failed on {name}: {ex.Message}");
            }
        }
        return true;
    }
}
