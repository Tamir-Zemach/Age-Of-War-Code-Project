using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Unit", order = 1)]
public class Unit : ScriptableObject
{
    [Tooltip("The prefab to instansiate when deplyed")]
    public GameObject _characterPrefab;
    [Tooltip("How much money needed to deploy the character")]
    public int _cost;
    [Tooltip("How much time passed between the moment the button is pressed and the character is deployed")]
    public float _deployDelayTime;
}
