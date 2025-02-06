using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance != null && _instance != (T)(object)this)
        {
            //DestroyImmediate(gameObject);
            return;
        }

        _instance = (T)(object)this;
        //DontDestroyOnLoad(gameObject);
        InAwake();
    }

    protected virtual void InAwake()
    {
        
    }
}


