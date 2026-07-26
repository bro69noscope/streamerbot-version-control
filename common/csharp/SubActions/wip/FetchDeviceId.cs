using System;
using System.IO;
using System.Text.Json;
using Streamer.bot.Plugin.Interface;

public class CPHInline
{
    public bool Execute()
    {
        var payload = new System.Collections.Generic.Dictionary<string, object>
        {
            { "inputName", "capture_facecam" },
            { "propertyName", "video_device_id" },
        };

        string response = CPH.ObsSendRaw(
            "GetInputPropertiesListPropertyItems",
            JsonSerializer.Serialize(payload)
        );

        string outputDir = @"C:\temp\streamerbot";
        Directory.CreateDirectory(outputDir);

        string outputPath = Path.Combine(outputDir, "device_list.json");

        File.WriteAllText(outputPath, response);

        CPH.LogInfo($"Wrote device list to {outputPath}");

        return true;
    }
}
