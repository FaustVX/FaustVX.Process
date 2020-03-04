using System.Collections.Generic;
using System.Diagnostics;
using Proc = System.Diagnostics.Process;
using System.IO;
using System.Linq;

namespace FaustVX.Process
{
    public static class Process
    {
        public delegate ProcessStartInfo GetProcessStartInfo(string command, params string?[] parameters);

        internal static string Join(this IEnumerable<string?> list, string separator = " ")
            => string.Join(separator, list.Where(s => s is string));
        
        public static GetProcessStartInfo CreateProcess(string program)
            => (command, parameters) => new ProcessStartInfo(program, command + (Join(parameters, " ") is string p ? $" {p}" : ""));

        public static ExitCode StartAndWaitForExit(this ProcessStartInfo startInfo)
        {
            var process = Proc.Start(startInfo);
            process.WaitForExit();
            return process.ExitCode;
        }

        public static ExitCode StartAndReadOutAndWaitForExit(this ProcessStartInfo startInfo, out StreamReader stdOut)
        {
            startInfo.RedirectStandardOutput = true;
            var process = Proc.Start(startInfo);
            stdOut = process.StandardOutput;
            process.WaitForExit();
            return process.ExitCode;
        }

        public static ExitCode StartAndReadErrAndWaitForExit(this ProcessStartInfo startInfo, out StreamReader stdErr)
        {
            startInfo.RedirectStandardError = true;
            var process = Proc.Start(startInfo);
            stdErr = process.StandardError;
            process.WaitForExit();
            return process.ExitCode;
        }

        public static ExitCode StartAndReadOutsAndWaitForExit(this ProcessStartInfo startInfo, out StreamReader stdOut, out StreamReader stdErr)
        {
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            var process = Proc.Start(startInfo);
            stdOut = process.StandardOutput;
            stdErr = process.StandardError;
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
