using BroStreamerTools.Logging;
using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    private const string TargetSceneArg = "hideFiltersTargetScene";

    public bool Execute()
    {
        string scene = args.ContainsKey(TargetSceneArg) ? args[TargetSceneArg].ToString() : "";

        if (string.IsNullOrWhiteSpace(scene))
        {
            BroLogger.Warning($"[HideFilters] Missing '{TargetSceneArg}' argument");
            return false;
        }

        HideAllFilters(scene);
        HideFiltersOnSceneItems(scene);
        return true;
    }

    private void HideAllFilters(string sourceName)
    {
        string json = CPH.ObsSendRaw(
            "GetSourceFilterList",
            new JObject { ["sourceName"] = sourceName }.ToString()
        );

        if (string.IsNullOrWhiteSpace(json))
        {
            BroLogger.Warning($"[HideFilters] No filter data for '{sourceName}'");
            return;
        }

        var root = JObject.Parse(json);

        if ((root["responseData"] ?? root)["filters"] is not JArray filters)
        {
            BroLogger.Warning($"[HideFilters] Unexpected response: {json}");
            return;
        }

        foreach (var filter in filters)
        {
            string name = (string)filter["filterName"];
            bool enabled = (bool?)filter["filterEnabled"] ?? false;

            if (!enabled)
                continue;

            CPH.ObsSetFilterState(sourceName, name, 1);
            BroLogger.Debug($"[HideFilters] Hid '{name}' on '{sourceName}'");
        }
    }

    private void HideFiltersOnSceneItems(string sceneName)
    {
        string json = CPH.ObsSendRaw(
            "GetSceneItemList",
            new JObject { ["sceneName"] = sceneName }.ToString()
        );

        if (string.IsNullOrWhiteSpace(json))
        {
            BroLogger.Warning($"[HideFilters] No scene items for '{sceneName}'");
            return;
        }

        var root = JObject.Parse(json);

        if ((root["responseData"] ?? root)["sceneItems"] is not JArray items)
        {
            BroLogger.Warning($"[HideFilters] Unexpected response: {json}");
            return;
        }

        foreach (var item in items)
        {
            string sourceName = (string)item["sourceName"];

            if (string.IsNullOrWhiteSpace(sourceName))
                continue;

            HideAllFilters(sourceName);
        }
    }
}
