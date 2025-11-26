#if UNITY_EDITOR_WIN
using UnityEditor;
using UnityEditor.Callbacks;
using System.Diagnostics;

public class PostBuildMac
{
    [PostProcessBuild(1)]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
        if (target == BuildTarget.StandaloneOSX)
        {
            // Make the .app bundle executable
            string appName = System.IO.Path.GetFileName(path);
            string executable = path + "/Contents/MacOS/" + appName.Replace(".app", "");

            // Use WSL or Git Bash to set permissions
            SetExecutablePermissions(executable);
        }
    }

    private static void SetExecutablePermissions(string path)
    {
        // This requires WSL (Windows Subsystem for Linux) or Git Bash
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "wsl";
            psi.Arguments = $"chmod +x '{path.Replace("\\", "/")}'";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            Process process = Process.Start(psi);
            process.WaitForExit();
        }
        catch
        {
            UnityEngine.Debug.LogWarning("Could not set executable permissions. Mac users may need to run: chmod +x on the executable.");
        }
    }
}
#endif