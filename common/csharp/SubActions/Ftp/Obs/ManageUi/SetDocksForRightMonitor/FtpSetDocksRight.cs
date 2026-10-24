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
                    ""filename"": ""right_monitor_ftp_1080p_100%""
                }
            }",
            0
        );
        return true;
    }
}
