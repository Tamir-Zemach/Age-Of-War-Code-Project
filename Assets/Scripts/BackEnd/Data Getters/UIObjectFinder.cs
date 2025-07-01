using UnityEngine;
using UnityEngine.UI;

public static class UIObjectFinder
{

    public static UnitDeployButton[] GetAllUnitDeployButtons(out Image[] buttonImages)
    {
        var buttons = Object.FindObjectsByType<UnitDeployButton>(FindObjectsSortMode.None);
        buttonImages = new Image[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttonImages[i] = buttons[i].GetComponent<Image>();
        }

        return buttons;
    }

    public static SpecialAttackButton GetSpecialAttackButton(out Image image)
    {
        var button = Object.FindFirstObjectByType<SpecialAttackButton>();

        if (button != null)
        {
            image = button.GetComponent<Image>();
        }
        else
        {
            Debug.LogWarning("SpecialAttackButton not found.");
            image = null;
        }

        return button;
    }
}