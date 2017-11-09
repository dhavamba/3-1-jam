using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeForCard : MonoBehaviour
{
    [SerializeField]
    private float time;

    public delegate void Action(int player, Card card);
    public static event Action Change;

    private void Awake()
    {
        Invoke("CreateCard", time);
    }

    private void CreateCard()
    {
        if (Change != null)
        {
            Change(0, Deck.Instance<Deck>().Pop(0));
            Change(1, Deck.Instance<Deck>().Pop(1));
        }
        Invoke("CreateCard", time);
    }
}
