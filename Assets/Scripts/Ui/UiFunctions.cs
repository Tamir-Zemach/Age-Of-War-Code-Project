using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Enems;

public class UiFunctions : MonoBehaviour
{ 
[Tooltip("Scene build index must match this enum value." +
        " Make sure the corresponding scene is added to Build Settings.")]
[SerializeField] private LevelType _nextLevel;

public void LoadNextLevel()
    {
        int sceneIndex = (int)_nextLevel;
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning($"Scene index {sceneIndex} is out of range.");
        }
    }

    public LevelType GetLevelType() => _nextLevel;
}
