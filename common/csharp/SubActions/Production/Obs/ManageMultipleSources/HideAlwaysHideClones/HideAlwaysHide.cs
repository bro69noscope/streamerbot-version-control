// Required references to manually add (not included in our sbot dlls):
//   - System.Core.dll

using BroStreamerTools.Logging;
using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    const string Target = "clone__always_hide";

    public bool Execute()
    {
        CPH.TryGetArg("debugthis", out bool debugEnabled);

        var scenes = JObject.Parse(CPH.ObsSendRaw("GetSceneList", "{}", 0))["scenes"];
        if (debugEnabled)
        {
            BroLogger.Debug($"Scene count: {((JArray)scenes).Count}");
        }

        foreach (var s in scenes)
        {
            string sceneName = (string)s["sceneName"];
            try
            {
                HideIn(sceneName, false, debugEnabled);
            }
            catch (System.Exception ex)
            {
                BroLogger.Error($"Failed on scene {sceneName}: {ex.Message}");
            }
        }
        return true;
    }

    void HideIn(string sceneName, bool isGroup, bool debugEnabled)
    {
        string req = isGroup ? "GetGroupSceneItemList" : "GetSceneItemList";
        string payload = new JObject { ["sceneName"] = sceneName }.ToString();
        string raw = CPH.ObsSendRaw(req, payload, 0);
        var items = JObject.Parse(raw)["sceneItems"];

        if (items == null)
        {
            if (debugEnabled)
            {
                BroLogger.Debug($"No sceneItems for {sceneName}. Raw: {raw}");
            }
            return;
        }

        foreach (var item in items)
        {
            string name = (string)item["sourceName"];
            if (debugEnabled)
            {
                BroLogger.Debug($"Checking {name} in {sceneName} (isGroup={isGroup})");
            }

            if (name == Target)
            {
                string set = new JObject
                {
                    ["sceneName"] = sceneName,
                    ["sceneItemId"] = (int)item["sceneItemId"],
                    ["sceneItemEnabled"] = false,
                }.ToString();
                CPH.ObsSendRaw("SetSceneItemEnabled", set, 0);
            }

            if (item.Value<bool?>("isGroup") ?? false)
                HideIn(name, true, debugEnabled);
        }
    }
}
