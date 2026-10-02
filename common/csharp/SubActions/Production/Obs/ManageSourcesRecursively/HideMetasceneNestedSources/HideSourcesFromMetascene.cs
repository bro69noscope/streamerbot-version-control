using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    private const string MetaScene = "scene__metascene";
    private const string Excluded = "scene__gameplay__fundamental_sources";

    public bool Execute()
    {
        foreach (var item in GetItems(MetaScene))
        {
            if ((string)item["sourceType"] != "OBS_SOURCE_TYPE_SCENE")
                continue;

            string nested = (string)item["sourceName"];
            if (nested == Excluded)
                continue;

            foreach (var source in GetItems(nested))
                SetEnabled(nested, (int)source["sceneItemId"], false);
        }

        return true;
    }

    private JArray GetItems(string scene)
    {
        string payload = new JObject { ["sceneName"] = scene }.ToString();
        string raw = CPH.ObsSendRaw("GetSceneItemList", payload, 0);

        if (string.IsNullOrEmpty(raw))
            return [];

        return (JArray)JObject.Parse(raw)["sceneItems"] ?? [];
    }

    private void SetEnabled(string scene, int itemId, bool enabled)
    {
        string payload = new JObject
        {
            ["sceneName"] = scene,
            ["sceneItemId"] = itemId,
            ["sceneItemEnabled"] = enabled,
        }.ToString();

        CPH.ObsSendRaw("SetSceneItemEnabled", payload, 0);
    }
}
