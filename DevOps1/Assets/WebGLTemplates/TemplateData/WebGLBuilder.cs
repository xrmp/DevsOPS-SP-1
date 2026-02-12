using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

namespace BuildScripts
{
    public static class WebGLBuilder
    {
        [MenuItem("Build/Build WebGL")]
        public static void BuildWebGL()
        {
            Debug.Log("Starting WebGL build...");

            // Переключаем платформу на WebGL если нужно
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
            }

            // Путь для сохранения билда
            string buildPath = Path.Combine(Application.dataPath, "../Builds/WebGL");

            // Создаем папку, если она не существует
            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            // Получаем все сцены из Build Settings
            string[] scenes = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                scenes[i] = EditorBuildSettings.scenes[i].path;
            }

            // Настройки сборки
            BuildPlayerOptions buildOptions = new BuildPlayerOptions();
            buildOptions.scenes = scenes;
            buildOptions.locationPathName = buildPath;
            buildOptions.target = BuildTarget.WebGL;
            buildOptions.options = BuildOptions.None;

            // Пытаемся установить кастомный шаблон
            SetCustomWebGLTemplate();

            // Выполняем сборку
            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"✅ WebGL Build succeeded! Size: {summary.totalSize} bytes");
                Debug.Log($"📁 Build saved at: {buildPath}");

                // Проверяем использование кастомного HTML
                CheckCustomHTML(buildPath);
            }
            else
            {
                Debug.LogError("❌ WebGL Build failed!");
            }
        }

        private static void SetCustomWebGLTemplate()
        {
            // Проверяем наличие кастомного шаблона
            string templatePath = "WebGLTemplates/CustomWebGLTemplate";
            string fullTemplatePath = Path.Combine(Application.dataPath, "../" + templatePath);

            if (Directory.Exists(fullTemplatePath))
            {
                // Устанавливаем кастомный шаблон
                PlayerSettings.WebGL.template = "PROJECT:CustomWebGLTemplate";
                Debug.Log("🎨 Custom WebGL Template selected: CustomWebGLTemplate");
            }
            else
            {
                Debug.LogWarning("⚠️ Custom WebGL Template not found. Using default template.");
                Debug.Log($"   Expected path: {fullTemplatePath}");
            }
        }

        private static void CheckCustomHTML(string buildPath)
        {
            string indexHtmlPath = Path.Combine(buildPath, "index.html");

            if (File.Exists(indexHtmlPath))
            {
                string htmlContent = File.ReadAllText(indexHtmlPath);

                // Проверяем наличие признаков кастомного шаблона
                if (htmlContent.Contains("CustomWebGLTemplate") ||
                    htmlContent.Contains("custom-template") ||
                    htmlContent.Contains("custom-header"))
                {
                    Debug.Log("✅ Custom HTML template is being used.");
                }
                else
                {
                    Debug.LogWarning("⚠️ Custom HTML template might not be properly applied.");
                    Debug.Log("   Check if your template is in the correct folder: WebGLTemplates/CustomWebGLTemplate/");
                }
            }
            else
            {
                Debug.LogError("❌ index.html not found in build directory.");
            }
        }
    }
}