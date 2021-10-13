using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected bool dontDestroyOnLoad = true;
    protected virtual void Awake()
    {
        Init(dontDestroyOnLoad);
    }

    void Init(bool _dontDestroyOnLoad)
    {
        if (!Instance)
        {
            Instance = FindObjectOfType<T>();
            if(_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
