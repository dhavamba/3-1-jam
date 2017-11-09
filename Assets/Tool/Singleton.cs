using UnityEngine;
using System.Reflection;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static T _Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    T[] types = (T[])FindObjectsOfType(typeof(T));
                    if (types.Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return null;
                    }
                    else if(types.Length < 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            "- there isn't class in th Scene for Singleton!"+
                            " Reopening the scene might fix it.");
                        return null;
                    }
                    else
                    {
                        _instance = types[0];
                    }
                }
                return _instance;
            }
        }
    }

    public static F Instance<F>() where F : Singleton<T>
    {
        return _Instance as F;
    }

    public static T Instance()
    {
        return _Instance;
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}