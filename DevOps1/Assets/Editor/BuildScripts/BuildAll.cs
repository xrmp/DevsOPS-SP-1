using UnityEditor;
using UnityEngine;
using System.IO;

namespace BuildScripts
{
    public static class BuildAll
    {
        [MenuItem("Build/Build All")]
        public static void BuildAllPlatforms()
        {
            Debug.Log("=================================");
            Debug.Log("🚀 Starting build process for all platforms...");
            Debug.Log("=================================");

            // Собираем ПК версию
            PCBuilder.BuildPC();

            // Собираем Android версию
            AndroidBuilder.BuildAndroid();

            // Временно закомментировано до создания WebGLBuilder
            // WebGLBuilder.BuildWebGL();

            Debug.Log("=================================");
            Debug.Log("✅ All builds completed!");
            Debug.Log("=================================");

            // Открываем папку с билдами
#if UNITY_EDITOR_WIN
            string buildsPath = Path.Combine(Application.dataPath, "../Builds");
            if (Directory.Exists(buildsPath))
            {
                System.Diagnostics.Process.Start("explorer.exe", buildsPath.Replace("/", "\\"));
            }
#endif
        }
    }
}