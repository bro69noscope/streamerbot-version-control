using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using BroStreamerTools.Logging;
using Newtonsoft.Json.Linq;

namespace BroStreamerTools;

public static class AhkClient
{
    const string ServerKeyPath = "repository.python";
    const string CommandPath = "/ahk/command";

    public static bool Send(string command, bool debug = false)
    {
        var portsFile = RepoPaths.PortsFile;
        if (portsFile == null)
        {
            BroLogger.Error("AhkClient: ports file not found");
            return false;
        }

        var node = JObject.Parse(File.ReadAllText(portsFile)).SelectToken(ServerKeyPath);
        if (node == null)
        {
            BroLogger.Error($"AhkClient: key '{ServerKeyPath}' not found in {portsFile}");
            return false;
        }

        var url = $"ws://{(string)node["host"]}:{(string)node["port"]}{CommandPath}";

        using var ws = new ClientWebSocket();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            ws.ConnectAsync(new Uri(url), cts.Token).GetAwaiter().GetResult();

            var segment = new ArraySegment<byte>(Encoding.UTF8.GetBytes(command));
            ws.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token)
                .GetAwaiter()
                .GetResult();

            ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "done", cts.Token)
                .GetAwaiter()
                .GetResult();

            if (debug)
                BroLogger.Debug($"Sent '{command}' to {url}");
            return true;
        }
        catch (OperationCanceledException)
        {
            BroLogger.Error($"Timed out after 5s sending '{command}' to {url} ({ws.State})");
        }
        catch (WebSocketException ex)
        {
            BroLogger.Error($"WebSocket error sending '{command}' to {url}: {ex.Message}");
        }
        catch (Exception ex)
        {
            BroLogger.Error($"Unexpected error sending '{command}' to {url}: {ex}");
        }

        return false;
    }
}
