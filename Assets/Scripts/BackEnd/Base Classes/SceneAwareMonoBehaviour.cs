using UnityEngine;

using UnityEngine.SceneManagement;

/// <summary>
/// A base class for scripts that need to run code when a new scene loads.
/// Inherit from this to automatically respond when Unity loads a new scene.
/// </summary>
/// <remarks>
/// Override the method:
/// <para>
/// <c>InitializeOnSceneLoad</c>
/// </para>
/// to handle your scene-specific setup.
/// </remarks>
public abstract class SceneAwareMonoBehaviour<T> : PersistentMonoBehaviour<T> where T : MonoBehaviour
{
    protected virtual void OnEnable() => SceneManager.sceneLoaded += InitializeOnSceneLoad;
    protected virtual void OnDisable() => SceneManager.sceneLoaded -= InitializeOnSceneLoad;

    /// <summary>
    /// Must be implemented to handle scene load logic for your derived class.
    /// </summary>
    protected abstract void InitializeOnSceneLoad(Scene scene, LoadSceneMode mode);
}