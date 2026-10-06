using System;
using System.IO;
using System.Runtime.CompilerServices;
using BroStreamerTools.Logging;

namespace BroStreamerTools;

public static class RepoPaths
{
    const string RootEnvVar = "NLLS_ROOT";
    const string PortsEnvVar = "NLLS_PORTS_FILE";
    const string RootMarker = ".project-root";
    const string PortsRelPath = @"config\ports.json5";

    static string _root;
    static string _portsFile;

    public static string Root => _root ??= FindRoot();

    public static string PortsFile => _portsFile ??= FindPortsFile();

    static string FindPortsFile()
    {
        var env = Environment.GetEnvironmentVariable(PortsEnvVar);
        if (!string.IsNullOrEmpty(env))
        {
            if (File.Exists(env))
                return env;

            BroLogger.Warning($"{PortsEnvVar}='{env}' set but file does not exist, ignoring");
        }

        if (Root == null)
        {
            BroLogger.Error($"Ports file not found: {PortsEnvVar} unset or invalid, no repo root");
            return null;
        }

        return Path.Combine(Root, PortsRelPath);
    }

    static string FindRoot([CallerFilePath] string sourceFile = "")
    {
        var env = Environment.GetEnvironmentVariable(RootEnvVar);
        if (!string.IsNullOrEmpty(env))
        {
            if (File.Exists(Path.Combine(env, RootMarker)))
                return env;

            BroLogger.Warning($"{RootEnvVar}='{env}' set but no {RootMarker} there, ignoring");
        }

        if (string.IsNullOrEmpty(sourceFile))
        {
            BroLogger.Error(
                "CallerFilePath is empty (built with /pathmap or deterministic paths?)"
            );

            return null;
        }

        for (
            var dir = new DirectoryInfo(Path.GetDirectoryName(sourceFile));
            dir != null;
            dir = dir.Parent
        )
        {
            if (File.Exists(Path.Combine(dir.FullName, RootMarker)))
                return dir.FullName;
        }

        BroLogger.Error(
            $"No {RootMarker} found walking up from '{sourceFile}' "
                + $"(repo moved since build? "
                + $"rebuild the DLL or set {RootEnvVar})"
        );
        return null;
    }
}
