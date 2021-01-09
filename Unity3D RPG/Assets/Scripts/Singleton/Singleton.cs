using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _singleton;

    private static object _lock = new object();

    private static bool appIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (appIsQuitting)
            {
                Debug.LogError("[Singleton<" + typeof(T).ToString() + ">] : " +
                                "already destroyed on application quit");

                return null;
            }

            lock (_lock)
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<T>();

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton<" + typeof(T).ToString() + ">] : " +
                                        "singleton instance is duplicated");

                        return _singleton;
                    }

                    if (_singleton == null)
                    {
                        GameObject go = new GameObject();
                        _singleton = go.AddComponent<T>();
                        _singleton.name = "<Singleton> " + typeof(T).ToString();

                        DontDestroyOnLoad(_singleton);
                    }
                }

                return _singleton;
            }
        }
    }

    public virtual void OnApplicationQuit()
    {
        appIsQuitting = true;

        _singleton = null;
    }
}
