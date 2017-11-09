using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
