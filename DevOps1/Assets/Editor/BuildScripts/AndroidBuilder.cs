using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

namespace BuildScripts
{
    public static class AndroidBuilder
    {
        [MenuItem("Build/Build Android")]
        public static void BuildAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            }

            string buildPath = Path.Combine(Application.dataPath, "../Builds/Android");

            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            BuildPlayerOptions buildOptions = new BuildPlayerOptions();

            string[] scenes = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                scenes[i] = EditorBuildSettings.scenes[i].path;
            }

            string apkName = $"{Application.productName}_{PlayerSettings.bundleVersion}.apk";

            buildOptions.scenes = scenes;
            buildOptions.locationPathName = Path.Combine(buildPath, apkName);
            buildOptions.target = BuildTarget.Android;
            buildOptions.options = BuildOptions.None;

            PlayerSettings.Android.splitApplicationBinary = false;

            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Android Build succeeded: " + summary.totalSize + " bytes");
                Debug.Log("APK saved at: " + buildPath);
            }
            else if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("Android Build failed");
            }
        }
    }
}