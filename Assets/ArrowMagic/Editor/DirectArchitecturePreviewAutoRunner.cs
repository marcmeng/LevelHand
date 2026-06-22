using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    [InitializeOnLoad]
    public static class DirectArchitecturePreviewAutoRunner
    {
        private const string FlagFileName = "run_direct_architecture_preview.flag";
        private const string StartedFileName = "direct_architecture_preview_autorun_started.txt";
        private const string FinishedFileName = "direct_architecture_preview_autorun_finished.txt";
        private const string ErrorFileName = "direct_architecture_preview_autorun_error.txt";
        private const double PollIntervalSeconds = 2.0;
        private static double _nextPollTime;
        private static bool _running;

        private static string ProjectRoot => Directory.GetParent(Application.dataPath)?.FullName ?? Directory.GetCurrentDirectory();
        private static string RunDirectory => Path.Combine(ProjectRoot, ".codex-run");
        private static string FlagPath => Path.Combine(RunDirectory, FlagFileName);
        private static string LegacyFlagPath => Path.Combine(ProjectRoot, "Temp", FlagFileName);
        private static string StartedPath => Path.Combine(RunDirectory, StartedFileName);
        private static string FinishedPath => Path.Combine(RunDirectory, FinishedFileName);
        private static string ErrorPath => Path.Combine(RunDirectory, ErrorFileName);

        static DirectArchitecturePreviewAutoRunner()
        {
            EditorApplication.delayCall += TryRun;
            EditorApplication.update += Poll;
        }

        private static void Poll()
        {
            if (_running || EditorApplication.isCompiling || EditorApplication.isUpdating)
                return;

            double now = EditorApplication.timeSinceStartup;
            if (now < _nextPollTime)
                return;

            _nextPollTime = now + PollIntervalSeconds;
            TryRun();
        }

        private static void TryRun()
        {
            string flagPath = FindFlagPath();
            if (_running || flagPath == null)
                return;

            try
            {
                _running = true;
                Directory.CreateDirectory(RunDirectory);
                File.Delete(flagPath);
                if (File.Exists(LegacyFlagPath))
                    File.Delete(LegacyFlagPath);
                File.WriteAllText(StartedPath, DateTime.Now.ToString("O"));
                Debug.Log("[DirectArchitecturePreviewAutoRunner] Running direct architecture preview.");
                NoMaskProceduralGenerator.BuildDirectArchitecturePreviewPack();
                File.WriteAllText(FinishedPath, DateTime.Now.ToString("O"));
            }
            catch (Exception ex)
            {
                File.WriteAllText(ErrorPath, ex.ToString());
                Debug.LogException(ex);
            }
            finally
            {
                _running = false;
            }
        }

        private static string FindFlagPath()
        {
            if (File.Exists(FlagPath))
                return FlagPath;

            if (File.Exists(LegacyFlagPath))
                return LegacyFlagPath;

            return null;
        }
    }
}
