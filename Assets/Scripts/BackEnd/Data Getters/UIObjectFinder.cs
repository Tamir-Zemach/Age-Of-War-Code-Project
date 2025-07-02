using Assets.Scripts.Ui.TurretButton;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Utility class for locating UI components in the scene, such as unit deploy buttons or special attack buttons.
/// Provides helper methods to find objects and fetch their associated Image components.
/// </summary>
public static class UIObjectFinder
{

    public static UnitDeployButton[] GetAllUnitDeployButtons()
    {
        return Object.FindObjectsByType<UnitDeployButton>(FindObjectsSortMode.None);
    }

    public static SpecialAttackButton GetSpecialAttackButton()
    {
        return Object.FindFirstObjectByType<SpecialAttackButton>(); 
    }

    public static TurretButton GetTurretButton()
    {
        return Object.FindFirstObjectByType<TurretButton>();
    }
}