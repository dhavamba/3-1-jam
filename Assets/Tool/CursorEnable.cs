using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Block and the makes invisible the cursor mouse
public class CursorEnable : Singleton<CursorEnable>
{
    [SerializeField]
    private bool enable;   //is enable?

    /// <summary>
    /// Enable or not enable the mouse
    /// </summary>
    public bool Enable
    {
        set
        {
            enable = value;
            EnableMouse();
        }
    }

    // Use this for initialization
    private void Awake()
    {
        EnableMouse();
    }

    //enable or not enable the mouse
    private void EnableMouse()
    {
        Cursor.visible = enable;
        if (!enable)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
