using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    const string Scene = "scene__layer__eyetracker";
    const string Source = "capture__game__eyetracker";

    public bool Execute()
    {
        SetEnabled(2, true);
        SetEnabled(0, false);
        return true;
    }

    void SetEnabled(int conn, bool enabled)
    {
        string raw = CPH.ObsSendRaw(
            "GetSceneItemId",
            new JObject { ["sceneName"] = Scene, ["sourceName"] = Source }.ToString(),
            conn
        );
        int id = (int)JObject.Parse(raw)["sceneItemId"];

        CPH.ObsSendRaw(
            "SetSceneItemEnabled",
            new JObject
            {
                ["sceneName"] = Scene,
                ["sceneItemId"] = id,
                ["sceneItemEnabled"] = enabled,
            }.ToString(),
            conn
        );
    }
}
