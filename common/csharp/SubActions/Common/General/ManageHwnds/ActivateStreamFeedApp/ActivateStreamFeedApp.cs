using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

public partial class CPHInline
{
    public bool Execute()
    {
        using var ws = new ClientWebSocket();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        ws.ConnectAsync(new Uri("ws://127.0.0.1:8187"), cts.Token).GetAwaiter().GetResult();

        var bytes = Encoding.UTF8.GetBytes("ActivateStreamFeedApp");
        var segment = new ArraySegment<byte>(bytes);

        ws.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token).GetAwaiter().GetResult();

        ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "done", cts.Token)
            .GetAwaiter()
            .GetResult();

        return true;
    }
}
