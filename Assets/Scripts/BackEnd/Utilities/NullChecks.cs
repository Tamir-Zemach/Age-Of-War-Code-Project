using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class containing static methods for null and data integrity checks.
/// Use these to streamline and standardize error handling throughout the project.
/// </summary>
public static class NullChecks
{
    /// <summary>
    /// Checks whether either of the two provided data lists is null.
    /// </summary>
    /// <typeparam name="TData">The type of data items in the main list.</typeparam>
    /// <typeparam name="TLevelUpData">The type of items in the level-up data list.</typeparam>
    /// <param name="dataList">The main list of data items.</param>
    /// <param name="levelUpDataList">The corresponding level-up data list.</param>
    /// <returns>True if either list is null; otherwise false.</returns>
    public static bool DataListNullCheck<TData, TLevelUpData>(List<TData> dataList, List<TLevelUpData> levelUpDataList)
    {
        if (dataList == null || levelUpDataList == null)
        {
            Debug.LogWarning($"[ListNullCheck] Data list null: {dataList == null}, Level-up data list null: {levelUpDataList == null}");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks whether either of the two individual objects is null.
    /// </summary>
    /// <typeparam name="TData">The type of the main data object.</typeparam>
    /// <typeparam name="TLevelUpData">The type of the level-up data object.</typeparam>
    /// <param name="data">The main data object.</param>
    /// <param name="levelUpData">The corresponding level-up data object.</param>
    /// <returns>True if either object is null; otherwise false.</returns>
    public static bool DataNullCheck<TData, TLevelUpData>(TData data, TLevelUpData levelUpData)
    {
        if (data == null || levelUpData == null)
        {
            Debug.LogWarning($"[ObjectNullCheck] Data is null: {data == null}, Level-up data is null: {levelUpData == null}");
            return true;
        }
        return false;
    }
}