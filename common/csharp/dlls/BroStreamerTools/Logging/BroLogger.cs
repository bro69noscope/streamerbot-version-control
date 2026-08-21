using System;
using System.IO;

namespace BroStreamerTools.Logging;

public static class BroLogger
{
    private static readonly string LogFile = Path.Combine(
        Path.GetDirectoryName(typeof(BroLogger).Assembly.Location),
        "BroStreamerTools.log"
    );

    public static void Debug(string message)
    {
        Write("DEBUG", message);
    }

    public static void Info(string message)
    {
        Write("INFO", message);
    }

    public static void Warning(string message)
    {
        Write("WARNING", message);
    }

    public static void Error(string message)
    {
        Write("ERROR", message);
    }

    private static void Write(string level, string message)
    {
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + $"[{level}] {message}";
        File.AppendAllText(LogFile, line + Environment.NewLine);
    }
}
