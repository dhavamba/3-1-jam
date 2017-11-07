using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeForCard : MonoBehaviour
{
    [SerializeField]
    private float time;

    public delegate void Action(Card card);
    public static event Action Change;

    private void Awake()
    {
        Invoke("CreateCard", time);
    }

    private void CreateCard()
    {
        if (Change != null)
        {
            Change(Deck.Instance<Deck>().Pop());
        }
        Invoke("CreateCard", time);
    }
}
