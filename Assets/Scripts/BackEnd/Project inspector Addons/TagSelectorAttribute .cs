using UnityEngine;

public class TagSelectorAttribute : PropertyAttribute
{
    public string Tooltip { get; }

    public TagSelectorAttribute(string tooltip = "")
    {
        Tooltip = tooltip;
    }
}