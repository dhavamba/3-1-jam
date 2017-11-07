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
        DontDestroyOnLoad(gameObject);
    }
}
