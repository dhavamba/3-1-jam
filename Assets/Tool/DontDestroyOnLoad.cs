using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField]
    private bool allowDuplicate;
    private bool isFirst;

    private void Start()
    {
        if (!allowDuplicate)
        {
            EliminateDuplicate();
            SceneManager.sceneLoaded += EliminateDuplicate;
            isFirst = true;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void EliminateDuplicate(Scene s, LoadSceneMode l)
    {
        EliminateDuplicate();
    }

    private void OnDestroy()
    {
        if (isFirst)
        {
            SceneManager.sceneLoaded -= EliminateDuplicate;
        }
    }

    private void EliminateDuplicate()
    {
        DontDestroyOnLoad[] names = gameObject.FindOtherObjectsOfType<DontDestroyOnLoad>();

        foreach (DontDestroyOnLoad s in names)
        {
            if (s.gameObject.name == gameObject.name)
            {
                DestroyImmediate(s.gameObject);
            }
        }
    }
}
