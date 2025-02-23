#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BobaDrinkerPro.Editor
{

    public static class Build
    {

        [MenuItem("BobaDrinkerPro/Build/WebGL")]
        private static void BuildWebGL()
        {

            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Select(s => s.path).ToArray(),
                locationPathName = "Builds/WebGL",
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            });

            switch (report.summary.result)
            {
                case BuildResult.Succeeded:
                    Debug.Log("Build successful");

                    break;
                case BuildResult.Failed:
                    Debug.Log($"Build failed with {report.summary.totalErrors} errors.");

                    break;
            }

        }

    }

}

#endif
