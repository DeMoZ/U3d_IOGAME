// Класс для включения просмотра файла лога в iTunes для iPad

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class PostProcessBuildPlayer : MonoBehaviour
{
    private const string m_newName = "KiUSSPM";

    [PostProcessBuild]
    static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        Debug.Log(buildTarget);

        // Включение доступа к общим файлам приложения через iTunes для просмотра лога
        if (buildTarget == BuildTarget.iOS)
        {
            // Read plist
            var plistPath = Path.Combine(path, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            // Update value
            PlistElementDict rootDict = plist.root;
            rootDict.SetBoolean("UIFileSharingEnabled", true);

            // Write plist
            File.WriteAllText(plistPath, plist.WriteToString());
        }

        // Переименование exe файла собранной версии
        if (buildTarget == BuildTarget.StandaloneWindows || buildTarget == BuildTarget.StandaloneWindows64)
        {
            string buildDirectory = Path.GetDirectoryName(path) ?? string.Empty;
            string dataFolderPath = Path.Combine(buildDirectory, Application.productName + "_Data");

            Debug.Log(path);
            Debug.Log(dataFolderPath);

            if (File.Exists(path))
            {
                File.Move(path, Path.Combine(buildDirectory, m_newName + ".exe"));
            }

            if (Directory.Exists(dataFolderPath))
            {
                Directory.Move(dataFolderPath, Path.Combine(buildDirectory, m_newName + "_Data"));
            }
        }
    }
}