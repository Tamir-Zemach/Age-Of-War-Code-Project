using System;
using UnityEditor;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneReference
{
    public SceneAsset sceneAsset;

    public string GetScenePath()
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.GetAssetPath(sceneAsset);
#else
        return string.Empty;
#endif
    }

    public int GetBuildIndex()
    {
        string path = GetScenePath();
        return SceneUtility.GetBuildIndexByScenePath(path);
    }
}
