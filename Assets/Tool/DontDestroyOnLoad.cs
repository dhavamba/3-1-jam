using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField]
    private bool allowDuplicate;
    private bool isFirst;

    private void Awake()
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
                if (!s.isFirst)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(this);
                }
            }
        }
    }
}
