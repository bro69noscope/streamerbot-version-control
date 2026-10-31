using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace BroStreamerTools.Logging;

public static class BroLogger
{
    private static readonly string LogFile = Path.Combine(
        Path.GetDirectoryName(typeof(BroLogger).Assembly.Location),
        "BroStreamerTools.log"
    );

    public static void Debug(
        string message,
        [CallerFilePath] string file = "",
        [CallerMemberName] string member = ""
    )
    {
        Write("DEBUG", message, file, member);
    }

    public static void Info(
        string message,
        [CallerFilePath] string file = "",
        [CallerMemberName] string member = ""
    )
    {
        Write("INFO", message, file, member);
    }

    public static void Warning(
        string message,
        [CallerFilePath] string file = "",
        [CallerMemberName] string member = ""
    )
    {
        Write("WARNING", message, file, member);
    }

    public static void Error(
        string message,
        [CallerFilePath] string file = "",
        [CallerMemberName] string member = ""
    )
    {
        Write("ERROR", message, file, member);
    }

    private static void Write(string level, string message, string file, string member)
    {
        var source = Path.GetFileNameWithoutExtension(file);
        var line =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + $"[{level}] [{source}.{member}] {message}";
        File.AppendAllText(LogFile, line + Environment.NewLine);
    }
}
