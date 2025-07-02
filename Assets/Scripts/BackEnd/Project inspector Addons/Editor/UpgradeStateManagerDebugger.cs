using UnityEngine;
using UnityEditor;
using Assets.Scripts.Enems;

public class UpgradeStateManagerDebugger : EditorWindow
{
    private Vector2 _scroll;

    [MenuItem("Tools/Upgrade State Manager Debugger")]
    public static void ShowWindow()
    {
        GetWindow<UpgradeStateManagerDebugger>("Upgrade State Manager Debugger");
    }

    private void OnGUI()
    {
        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Enter Play Mode to inspect UpgradeStateManager data.", MessageType.Info);
            return;
        }

        var manager = UpgradeStateManager.Instance;
        if (manager == null)
        {
            EditorGUILayout.HelpBox("UpgradeStateManager not found.", MessageType.Warning);
            return;
        }

        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        DrawSection("Unit Stat Upgrade Costs", () =>
        {
            foreach (var kvp in manager.GetAllUnitStatUpgradeCosts())
                EditorGUILayout.LabelField($"{kvp.Key.Item1} / {kvp.Key.Item2}", kvp.Value.ToString());
        });

        DrawSection("Player Stat Upgrade Costs", () =>
        {
            foreach (var kvp in manager.GetAllPlayerUpgradeCosts())
                EditorGUILayout.LabelField($"{kvp.Key}", kvp.Value.ToString());
        });

        DrawSection("Unit Prefabs", () =>
        {
            foreach (var kvp in manager.GetAllUnitPrefabs())
                EditorGUILayout.ObjectField($"{kvp.Key}", kvp.Value, typeof(GameObject), false);
        });

        DrawSection("Special Attack Prefabs", () =>
        {
            foreach (var kvp in manager.GetAllSpecialAttackPrefabs())
                EditorGUILayout.ObjectField($"{kvp.Key}", kvp.Value, typeof(GameObject), false);
        });

        DrawSection("Turret Prefabs", () =>
        {
            foreach (var kvp in manager.GetAllTurretPrefabs())
                EditorGUILayout.ObjectField($"{kvp.Key}", kvp.Value, typeof(GameObject), false);
        });

        EditorGUILayout.EndScrollView();
    }

    private void DrawSection(string title, System.Action drawContent)
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        drawContent?.Invoke();
        EditorGUI.indentLevel--;
    }
}