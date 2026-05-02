using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning($"Only 1 {typeof(T)} allowed!");
            Destroy(gameObject);
            return;
        }

        instance = this as T;
    }
}
