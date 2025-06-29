using Assets.Scripts.InterFaces;
using UnityEngine;

public abstract class PersistentMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour 
{
    public static T Instance;

    public virtual void InstantiateOneObject()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

    
    protected virtual void Awake()
    {
        InstantiateOneObject();
    }
}