using System;

namespace BroStreamerTools;

public static class MetasceneScenes
{
    public const string Parent = "scene__metascene";

    public static readonly string[] Essential =
    [
        "scene__alerts",
        "scene__gameplay__fundamental_sources",
    ];

    public static bool IsEssential(string sceneName) => Array.IndexOf(Essential, sceneName) >= 0;
}
