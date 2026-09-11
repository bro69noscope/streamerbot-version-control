public class CPHInline
{
    public bool Execute()
    {
        string scene = CPH.ObsGetCurrentScene();
        string source = "audio_mixer_5/6";
        int obsConnection = 0;

        CPH.ObsSourceUnMute(scene, source, obsConnection);

        return true;
    }
}
