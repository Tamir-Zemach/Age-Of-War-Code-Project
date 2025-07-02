using Assets.Scripts.Enems;
using Assets.Scripts.InterFaces;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Unit", order = 1)]
public class UnitData : ScriptableObject, IUpgradable<UnitType>
{
    [Tooltip("The Unit Type:")]
    [SerializeField] private UnitType _unitType;

    [Tooltip("Is The Unit Friendly or an Enemy")]
    [SerializeField] private bool _isFriendly;



    private AgeStageType _stageType;
    public int AgeStage => (int)_stageType;
    public UnitType Type => _unitType;
    public bool IsFriendly => _isFriendly;

    [Tooltip("The prefab to instansiate when deplyed")]
    [SerializeField] private GameObject _unitPrefab;
    public GameObject Prefab => _unitPrefab;

    public void SetPrefab(GameObject prefab)
    {
        _unitPrefab = prefab;
    }


    public Sprite _spriteForUi;

    [Tooltip("How much money needed to deploy the Unit")]
    public int _cost;
    [Tooltip("How much time passed between the moment the button is pressed and the character is deployed")]
    public float _deployDelayTime;

    [Header("Enemy Unit Properties")]
    [Tooltip("How much money the player gains when this Unit is Destroyed")]
    public int _moneyWhenKilled;


    [Header("Tags")]
    [Tooltip("The unit will walk toward the GameObject with this tag:")]
    [SerializeField, TagSelector] public string _oppositeBaseTag;

    [Tooltip("The Unit will stop  when it sees the GameObject with this tag: ")]
    [SerializeField, TagSelector] public string _friendlyUnitTag;

    [Tooltip("The Unit will stop and attack when it encounters the GameObject with this tag:")]
    [SerializeField, TagSelector] public string _oppositeUnitTag;

    [Tooltip("The Unit will stop and attack when it encounters the GameObject with this Layer:")]
    [SerializeField, TagSelector] public LayerMask _enemyCharacterMask;



    [Header("Unit Parameters")]
    [Tooltip("How far the unit detects opposite unit")]
    public int _range;
    [Tooltip("The speed Of the Unit")]
    public float _speed = 1;
    [Tooltip("The Health Of the Unit")]
    public int _health = 1;
    [Tooltip("How much every Hit will give damage")]
    public int _strength = 1;
    [Tooltip("The amount of time before a Unit Attacks (when lower its faster)")]
    public float _initialAttackDelay = 1;




    [Header("Debug Properties")]
    [Tooltip("The size of the box the Unit casts")]
    public Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);

    [Tooltip("The color of the box the Unit casts")]
    public Color boxColor = Color.red;

    [Tooltip("How far the Unit detects other Friendly Units")]
    public float _rayLengthForFriendlyUnit = 2;

}
