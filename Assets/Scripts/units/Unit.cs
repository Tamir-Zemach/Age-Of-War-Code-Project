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

    [Tooltip("The Unit will stop  when it sees the GameObject with this tag: ")]
    [SerializeField, TagSelector] public string _friendlyCharacterTag;

    [Tooltip("The Unit will stop and attack when it encounters the GameObject with this tag:")]
    [SerializeField, TagSelector] public string _enemyCharacterTag;

    [Tooltip("The Unit will stop and attack when it encounters the GameObject with this Layer:")]
    [SerializeField, TagSelector] public LayerMask _enemyCharacterMask;

    [Header("BoxCast Parameters")]
    [Tooltip("How far the character detects other characters")]
    public float _range;

    [Header("Character Parameters")]
    [Tooltip("The speed Of the Unit")]
    public float _speed = 1;
    [Tooltip("The Health Of the Unit")]
    public int _health = 1;
    [Tooltip("How much every Hit will give damage")]
    public int _strength = 1;
    [Tooltip("The amount of time before a Unit Attacks (when lower its faster)")]
    public float _insialAttackDelay = 1;
}
