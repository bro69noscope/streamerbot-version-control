using System;

public class CPHInline
{
    public bool Execute()
    {
        var start = CPH.GetGlobalVar<DateTime>("BRBStartTime", true);
        var duration = CPH.GetGlobalVar<string>("BRBDuration", true);
        var elapsed = DateTime.Now - start;

        var durationText = int.TryParse(duration, out var mins)
            ? $"{mins} {(mins == 1 ? "min" : "mins")}"
            : $"{duration} mins";

        var text = $"BRB in {durationText}\n" + $"Elapsed: {elapsed:mm\\:ss}";

        CPH.ObsSetGdiText("scene_lounge", "brb", text);
        return true;
    }
}
