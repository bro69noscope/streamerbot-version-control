using Newtonsoft.Json.Linq;

public partial class CPHInline
{
    const string InputName = "capture__window__dynamic_hwnd";
    const int ObsConnection = 0;

    static string Enc(string s)
    {
        return s.Replace("#", "#23").Replace(":", "#3A");
    }

    public bool Execute()
    {
        string title = args["title"].ToString();
        string cls = args["class"].ToString();
        string exe = args["exe"].ToString();
        int prio = int.Parse(args["priority"].ToString());

        var body = new JObject
        {
            ["inputName"] = InputName,
            ["inputSettings"] = new JObject
            {
                ["window"] = Enc(title) + ":" + Enc(cls) + ":" + Enc(exe),
                ["priority"] = prio,
            },
            ["overlay"] = true,
        };

        CPH.ObsSendRaw(
            "SetInputSettings",
            body.ToString(Newtonsoft.Json.Formatting.None),
            ObsConnection
        );
        return true;
    }
}
