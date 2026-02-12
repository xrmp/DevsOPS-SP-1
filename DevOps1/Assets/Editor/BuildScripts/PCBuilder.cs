using UnityEditor;
using UnityEngine;
using System.IO;

namespace BuildScripts
{
    public static class PCBuilder
    {
        [MenuItem("Build/Build PC")]
        public static void BuildPC()
        {
            // Путь для сохранения билда
            string buildPath = Path.Combine(Application.dataPath, "../Builds/PC");

            // Создаем папку, если она не существует
            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            // Настройки сборки
            BuildPlayerOptions buildOptions = new BuildPlayerOptions();

            // Получаем все сцены из Build Settings
            string[] scenes = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                scenes[i] = EditorBuildSettings.scenes[i].path;
            }

            buildOptions.scenes = scenes;
            buildOptions.locationPathName = Path.Combine(buildPath, Application.productName + ".exe");
            buildOptions.target = BuildTarget.StandaloneWindows64;
            buildOptions.options = BuildOptions.None;

            // Выполняем сборку
            BuildPipeline.BuildPlayer(buildOptions);

            Debug.Log("PC Build completed at: " + buildPath);
        }
    }
}