using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShowPanels : MonoBehaviour
{
    [SerializeField]
    private GameObject[] optionsPanels;

    public void Show(GameObject obj)
    {
        foreach (GameObject o in optionsPanels)
        {
            o.SetActive(false);
        }
        obj.SetActive(true);
    }
}
