namespace BroStreamerTools.Obs;

public static class ObsConn
{
    // The first connection of whichever profile runs the action: obs_conn__prod in the production
    // profile, the only one (ftp_obs_conn__ftp) in the ftp profile. For Common and Ftp sub-actions.
    public const int Default = 0;

    // Production profile only, in the order of production/data/obs.vcs-template.json.
    public const int Prod = 0;
    public const int Vcam = 1;
    public const int Ftp = 2;
}
