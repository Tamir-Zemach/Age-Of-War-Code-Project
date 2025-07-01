/*
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Enems;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelLoader levelLoader = (LevelLoader)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("🔍 Level-to-Scene Mapping", EditorStyles.boldLabel);

        foreach (LevelType level in levelLoader.GetLevels())
        {
            int sceneIndex = (int)level;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Level:", level.ToString());
            EditorGUILayout.LabelField("Build Index:", sceneIndex.ToString());

            if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                EditorGUILayout.HelpBox(
                    $"Scene index {sceneIndex} is not present in Build Settings.",
                    MessageType.Warning);
            }
            else
            {
                string path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
                EditorGUILayout.LabelField("Scene Path:", path);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
    }
}

*/