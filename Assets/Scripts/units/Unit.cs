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

    [Header("Tags")]
    [Tooltip("The character will walk toward the GameObject with this tag:")]
    [SerializeField, TagSelector] public string _baseTag;

    [Tooltip("The character ")]
    [SerializeField, TagSelector] public string _friendlyCharacterTag;

    [Tooltip("The character will stop when it encounters the GameObject with this tag:")]
    [SerializeField, TagSelector] public string _enemyCharacterTag;


    [Header("BoxCast Parameters")]
    [Tooltip("How far the character detects other characters")]
    public float _raylength;

    [Header("Character Parameters")]
    public float _speed = 1;
    public int _health = 1;
    public int _strength = 1;
    public float _insialAttackDelay = 1;
    public float _attackPeriod = 0.2f;
}
