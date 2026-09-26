public partial class CPHInline
{
    public bool Execute()
    {
        string scene = CPH.ObsGetCurrentScene();
        string source = "audio__mixer_11/12";
        int obsConnection = 0;

        CPH.ObsSourceMute(scene, source, obsConnection);

        return true;
    }
}
