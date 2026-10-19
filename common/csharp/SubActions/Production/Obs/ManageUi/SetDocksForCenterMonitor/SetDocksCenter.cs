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
                    ""filename"": ""center_monitor_production_1080p""
                }
            }",
            0
        );
        return true;
    }
}
