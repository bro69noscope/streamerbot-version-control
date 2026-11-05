using BroStreamerTools.Obs;
using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    private const string MetaScene = "scene__metascene";

    public bool Execute()
    {
        string json = CPH.ObsSendRaw(
            "GetSceneItemList",
            new JObject { ["sceneName"] = MetaScene }.ToString(),
            ObsConn.Prod
        );

        foreach (var item in JObject.Parse(json)["sceneItems"])
        {
            string source = (string)item["sourceName"];
            CPH.ObsSetSourceVisibility(MetaScene, source, true, ObsConn.Prod);
        }

        return true;
    }
}
