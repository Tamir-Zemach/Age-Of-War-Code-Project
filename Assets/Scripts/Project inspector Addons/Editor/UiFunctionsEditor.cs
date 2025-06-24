using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Enems; // Make sure this namespace matches where LevelType is defined

[CustomEditor(typeof(UiFunctions))]
public class UiFunctionsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UiFunctions uiFunctions = (UiFunctions)target;
        int levelIndex = (int)uiFunctions.GetLevelType();

        if (levelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            EditorGUILayout.HelpBox(
                $"Scene index {levelIndex} is not present in Build Settings.",
                MessageType.Warning);
        }
        else
        {
            string path = SceneUtility.GetScenePathByBuildIndex(levelIndex);
            EditorGUILayout.LabelField("Build Scene Path:", path);
        }
    }
}