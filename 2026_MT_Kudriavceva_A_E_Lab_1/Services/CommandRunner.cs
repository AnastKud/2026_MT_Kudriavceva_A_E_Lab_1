using System.Diagnostics;
using System.Reflection.Emit;
namespace Core;

public class CommandRunner
{
    public int Run(string command, string args, string workingDir, Logger logger)
    {
        using var process = new Process();
        process.StartInfo.FileName = command;
        process.StartInfo.Arguments = args;
        process.StartInfo.WorkingDirectory = workingDir;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;

        try
        {
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            ArgumentNullException.ThrowIfNull(logger);
            if (!string.IsNullOrWhiteSpace(output))
                logger.Info(output.Trim());

            if (!string.IsNullOrWhiteSpace(error))
                logger.Error(error.Trim());

            return process.ExitCode;
        }
        catch (InvalidOperationException ex)
        {
            ArgumentNullException.ThrowIfNull(logger);
            logger.Error(ex.Message);
            return -1;
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            ArgumentNullException.ThrowIfNull(logger);
            logger.Error(ex.Message);
            return -1;
        }
        catch (ArgumentException ex)
        {
            ArgumentNullException.ThrowIfNull(logger);
            logger.Error(ex.Message);
            return -1;
        }
    }
}