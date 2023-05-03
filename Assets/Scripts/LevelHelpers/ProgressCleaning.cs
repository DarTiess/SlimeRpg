#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class ProgressCleaning : EditorWindow
{
    [MenuItem("ClearProgress/Clear")]
    static void Init()
    {
        PlayerPrefs.DeleteAll();
        //Directory.Delete(UnityEngine.Application.persistentDataPath + "//" + SDev.FileSaveUtil.AppPath.PersistentDataPath.ToString(),true);
        Debug.Log("Progress Clear");
    }
}
#endif