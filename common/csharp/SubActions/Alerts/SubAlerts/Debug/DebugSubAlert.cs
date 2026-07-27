using System;
using BroStreamerTools.Logging;

public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("userName", out string gifter);
        CPH.TryGetArg("recipientUserName", out string recipient);
        CPH.TryGetArg("gifts", out string giftCount);
        CPH.TryGetArg("tier", out string tier);
        CPH.TryGetArg("isAnonymous", out string isAnon);

        BroLogger.Info($"userName: '{gifter}'");
        BroLogger.Info($"recipientUserName: '{recipient}'");
        BroLogger.Info($"gifts: '{giftCount}'");
        BroLogger.Info($"tier: '{tier}'");
        BroLogger.Info($"isAnonymous: '{isAnon}'");

        return true;
    }
}
