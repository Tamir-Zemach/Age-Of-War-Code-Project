

using Assets.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//TODO: first level specail implement 

public class LevelLoader : PersistentMonoBehaviour<LevelLoader>
{
    [SerializeField]
    private List<SceneReference> scenes = new();

    private int _currentLevelIndex = 0;
    public int LevelIndex => _currentLevelIndex;

    public void LoadNextLevel()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex >= scenes.Count)
        {
            Debug.Log("No more levels to load.");
            return;
        }

        int buildIndex = scenes[_currentLevelIndex].GetBuildIndex();

        if (buildIndex < 0)
        {
            Debug.LogWarning("Scene not in Build Settings: " +
                scenes[_currentLevelIndex].sceneAsset?.name);
            return;
        }
        SceneManager.LoadScene(buildIndex);
    }

    public void ReloadCurrentLevel()
    {
        int buildIndex = scenes[_currentLevelIndex].GetBuildIndex();
        if (buildIndex >= 0)
            SceneManager.LoadScene(buildIndex);
    }

    public SceneReference GetCurrentSceneReference() => scenes[_currentLevelIndex];
}








/*
using Assets.Scripts.Enems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : PersistentMonoBehaviour<LevelLoader>
{
    private int _currentLevelIndex = 0;
    public int LevelIndex => _currentLevelIndex;

    [Tooltip("Scene build index must match this enum value. " +
        "Ensure scenes are added to Build Settings.")]
    [SerializeField] private LevelType[] _levels;

    private Dictionary<LevelType, int> _levelSceneIndexMap = new Dictionary<LevelType, int>();

    private void Start()
    {
        InitializeLevelSceneMap();
    }

    private void InitializeLevelSceneMap()
    {
        foreach (LevelType level in _levels)
        {
            _levelSceneIndexMap[level] = (int)level; // cast enum to int assuming it matches build index
        }
    }

    public void LoadNextLevel()
    {
        if (_currentLevelIndex + 1 >= _levels.Length)
        {
            Debug.Log("No more levels to load.");
            return;
        }

        _currentLevelIndex++;
        LevelType nextLevel = _levels[_currentLevelIndex];

        if (_levelSceneIndexMap.TryGetValue(nextLevel, out int sceneIndex))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("Scene index for the next level not found.");
        }
    }
    public LevelType[] GetLevels() => _levels;
}
*/










