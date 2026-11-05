using BroStreamerTools.Obs;

public partial class CPHInline
{
    public bool Execute()
    {
        CPH.ObsSendRaw(
            "CallVendorRequest",
            @"{
                ""vendorName"": ""jrDockie"",
                ""requestType"": ""LoadDockset"",
                ""requestData"": {
                    ""filename"": ""center_monitor_ftp_1080p_150%""
                }
            }",
            ObsConn.Default
        );
        return true;
    }
}
