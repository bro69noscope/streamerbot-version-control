using System;

namespace BroStreamerTools.Obs;

public static class MetasceneScenes
{
    public const string Parent = "scene__metascene";

    public static readonly string[] Essential =
    [
        "scene__layer__alerts",
        "scene__layer_ig__fundamental_sources",
    ];

    public static bool IsEssential(string sceneName) => Array.IndexOf(Essential, sceneName) >= 0;
}
