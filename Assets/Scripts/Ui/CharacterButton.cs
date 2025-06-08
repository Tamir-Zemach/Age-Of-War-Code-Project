using Assets.Scripts;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [Tooltip("The prefab to instansiate when deplyed")]
    public GameObject _characterPrefab;
    [Tooltip("How much money needed to deploy the character")]
    public int _cost;
    [Tooltip("How much time passed between the moment the button is pressed and the character is deployed")]
    public float _deployDelayTime;
    [Tooltip("Text to dipsplay the queue index, needs to be child of this GameObject")]
    [SerializeField] private TextMeshProUGUI _queueCountText;
    private CharacterButton[] _queueArray;
    private CharacterButton _assignedCharacter; 
    private void Awake()
    {
        _assignedCharacter = GetComponent<CharacterButton>();
        _queueCountText = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.OnQueueChanged += UpdateQueueIndex;
    }
    private void Start()
    {
        UpdateQueueIndex();
    }
    private void OnDestroy()
    {
        GameManager.OnQueueChanged -= UpdateQueueIndex;
    }

    public void UpdateQueueIndex()
    {
        NullChecksForSafety();
        // Filter queue to only include characters matching _assignedCharacter
        _queueArray = GameManager.Instance.characterQueue.Where(c => c == _assignedCharacter).ToArray();

        _queueCountText.text = _queueArray.Length > 0 ? $"+ {_queueArray.Length}" : "";
    }

    private void NullChecksForSafety()
    {
        if (_assignedCharacter == null || GameManager.Instance == null || GameManager.Instance.characterQueue == null)
        {
            _queueCountText.text = "Error: Queue not initialized";
            return;
        }
    }
    public void OnButtonPress()
    {
        GameManager.Instance.CharacterButtonPressed(this);
    }

    public bool CanDeploy()
    {
        return PlayerCurrency.Instance.Money >= _cost;
    }

}



