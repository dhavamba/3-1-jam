using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSelectedEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject flag;
    private GameObject changeSelected;

    private ExtendStack<GameObject> cascadeUI;

    public GameObject PreSelect
    {
        get
        {
            if (cascadeUI.IsEmpty())
            {
                return null;
            }
            return cascadeUI.Peek();
        }
    }

    private void Update()
    {
        if (changeSelected)
        {
            EventSystem.current.SetSelectedGameObject(changeSelected);
            changeSelected = null;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        cascadeUI = new ExtendStack<GameObject>();
    }

    public void ChangeSelectNext(GameObject obj)
    {
        if (obj != EventSystem.current.currentSelectedGameObject)
        {
            cascadeUI.Push(EventSystem.current.currentSelectedGameObject);
            changeSelected = obj;
        }
    }

    public void ChangeInPreSelect()
    {
        if (!cascadeUI.IsEmpty())
        {
            EventSystem.current.SetSelectedGameObject(cascadeUI.Pop());
        }
    }
}