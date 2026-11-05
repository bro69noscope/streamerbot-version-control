using System;
using BroStreamerTools;
using BroStreamerTools.Logging;
using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    const string ParentScene = MetasceneScenes.Parent;

    public bool Execute()
    {
        CPH.TryGetArg("sbArgDebugThis", out bool debugEnabled);

        string raw = CPH.ObsSendRaw(
            "GetSceneItemList",
            new JObject { ["sceneName"] = ParentScene }.ToString(),
            ObsConn.Prod
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
                CPH.ObsSendRaw("SetSceneItemEnabled", set, ObsConn.Prod);
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
