
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnButton : MonoBehaviour
{
    
    //private Player player;
    private ChangeSelectedEvent selectedEvent;
    private ShowPanels panels;

    private void Awake()
    {
       // player = ReInput.players.GetPlayer(0);
        selectedEvent = GameObject.FindObjectOfType<ChangeSelectedEvent>();
        panels = GameObject.FindObjectOfType<ShowPanels>();
    }

    private void Update()
    {
        /*
        if (player.GetButtonDown("Return") && selectedEvent.PreSelect != null)
        {
            panels.Show(selectedEvent.PreSelect.transform.parent.gameObject);
            selectedEvent.ChangeInPreSelect();
        }
        */
    }
}
