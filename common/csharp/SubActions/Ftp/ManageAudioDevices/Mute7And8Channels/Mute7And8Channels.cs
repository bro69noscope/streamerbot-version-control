public partial class CPHInline
{
    public bool Execute()
    {
        string scene = CPH.ObsGetCurrentScene();
        string source = "audio_mixer_7/8";
        int obsConnection = 0;

        CPH.ObsSourceMute(scene, source, obsConnection);

        return true;
    }
}
