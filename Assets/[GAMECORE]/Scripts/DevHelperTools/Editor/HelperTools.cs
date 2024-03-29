using System;
using System.IO;
using System.Reflection;
using GAME.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.tvOS;

namespace Scripts.BaseGameScripts.DevHelperTools.Editor
{
    public class HelperTools
    {
        public static Action onDeletedSavedFiles;
        private static string s_path;

        [MenuItem("Developer Tools/Clear Console _c")] // Clear Console added
        private static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            if (method != null)
                method.Invoke(new object(), null);
        }

        [MenuItem("Developer Tools/Screen Shoot _b")]
        private static void ScreenShot()
        {
            PathCalculator();
            
            var iScrShotNo = PlayerPrefs.GetInt("iScrShotNo", 0);
            
            ScreenCapture.CaptureScreenshot(s_path + "\\s_" + iScrShotNo + ".png");
            
            iScrShotNo++;
            
            PlayerPrefs.SetInt("iScrShotNo", iScrShotNo);
        }

        [MenuItem("Developer Tools/Start Level 1 _m")]
        private static void StartLevel1()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = true;
#endif
            SceneManager.LoadScene(Defs.SCENE_NAME_LOADER);
        }
        
        [MenuItem("Developer Tools/Force Cleanup NavMesh")]
        public static void ForceCleanupNavMesh()
        {
            if (Application.isPlaying)
                return;
 
            NavMesh.RemoveAllNavMeshData();
        }

        private static void PathCalculator()
        {
            var size = Screen.height.ToString();

            if (size.Contains("2208"))
                size = "ip5";
            else if (size.Contains("2688"))
                size = "ip6";
            else if (size.Contains("2732")) 
		        size = "ipad";

            if(SystemInfo.deviceType == DeviceType.Desktop && SystemInfo.deviceName.Contains("mac") || SystemInfo.deviceName.Contains("Mac"))
                s_path = Application.dataPath + "//SS//" + size;
            else
                s_path = Application.dataPath + "\\..\\SS\\" + size;


            if (!Directory.Exists(s_path))
                Directory.CreateDirectory(s_path);
        }

        [MenuItem("Developer Tools/Delete Saved Files _d")]
        private static void DeleteSavedFiles()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}