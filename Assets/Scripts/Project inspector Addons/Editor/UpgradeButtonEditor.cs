
using Assets.Scripts.Enems;
using UnityEditor;

[CustomEditor(typeof(UpgradeButton))]
public class UpgradeButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var unitTypeProp = serializedObject.FindProperty("unitType");
        var statTypeProp = serializedObject.FindProperty("statType");
        var statBonusProp = serializedObject.FindProperty("_statBonus");
        var statCostProp = serializedObject.FindProperty("_statCost");
        var statCostIncProp = serializedObject.FindProperty("_statCostInc");
        var attackDelayReductionProp = serializedObject.FindProperty("_attackDelayReductionPercent");

        EditorGUILayout.PropertyField(unitTypeProp);
        EditorGUILayout.PropertyField(statTypeProp);
        // Show the attack speed field only if statType == AttackSpeed
        if ((StatType)statTypeProp.enumValueIndex == StatType.AttackSpeed)
        {
            EditorGUILayout.PropertyField(attackDelayReductionProp);
        }
        else
        {
            EditorGUILayout.PropertyField(statBonusProp);
        }
        EditorGUILayout.PropertyField(statCostProp);
        EditorGUILayout.PropertyField(statCostIncProp);



        serializedObject.ApplyModifiedProperties();
    }
}