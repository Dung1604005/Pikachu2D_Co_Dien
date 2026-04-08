using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    
    protected static T instance;

    public static bool HasInstance => instance != null;

    public static T TryGetInstance()=> HasInstance ? instance: null;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if(instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if(!Application.isPlaying) return;

        if (instance == null)
        {
            
            instance = this as T;
        }
        else if (instance != this as T)
        {
            
            Destroy(gameObject); 
        }
    }
}
