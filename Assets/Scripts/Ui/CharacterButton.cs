using Assets.Scripts;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [Tooltip("Text to dipsplay the queue index, needs to be child of this GameObject")]
    [SerializeField] private TextMeshProUGUI _queueCountText;
    private Unit[] _queueArray;
    [SerializeField] private Unit _assignedUnit; 
    private void Awake()
    {
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
        if (NullChecksForSafety())
        {
            return;
        }

        // Filter queue to only include characters matching _assignedCharacter
        _queueArray = GameManager.Instance._unitQueue.Where(c => c == _assignedUnit).ToArray();

        _queueCountText.text = _queueArray.Length > 0 ? $"+ {_queueArray.Length}" : "";
    }

    private bool NullChecksForSafety()
    {
        if (_assignedUnit == null || GameManager.Instance == null || GameManager.Instance._unitQueue == null)
        {
            _queueCountText.text = "Error: Queue not initialized";
            return true;
        }
        else { return false; }
    }


}



