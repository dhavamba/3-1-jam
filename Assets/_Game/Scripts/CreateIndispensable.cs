using UnityEngine;

public class CreateIndispensable : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}