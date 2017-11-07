using UnityEngine;

public class CreateIndispensable : MonoBehaviour
{
    public GameObject[] gameObjects;

    private void Awake()
    {
        foreach (GameObject obj in gameObjects)
        {
            if (!GameObject.Find(obj.name))
            {
                ExtendGameObject.Instantiate(obj.name, null, obj);
            }
        }
    }
}