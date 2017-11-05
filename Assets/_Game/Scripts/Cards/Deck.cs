using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Singleton<Deck>
{
    public int maxDeck;
    private List<Card> deck;

    private void Awake()
    {
        deck = new List<Card>();
    }

    public delegate void Action(int number);
    public static event Action Change;

    private void RandomDeck()
    {
        List<Card> l = deck;

        for (int i = 0; i < maxDeck; i++)
        {
            int aux = Random.Range(0, deck.Count - 1);
            l.Add(deck[aux]);
            deck.RemoveAt(aux);
        }

        deck = l;
    }

    private void AddDeck(Card c)
    {
        if (deck.Count < maxDeck)
        {
            deck.Add(c);
            CallEvent();
        }
    }

    private void RemoveDeck(Card c)
    {
        int aux = deck.FindIndex(x => x == c);
        if (aux != -1)
        {
            deck.RemoveAt(aux);
            CallEvent();
        }
    }

    private void CallEvent()
    {
        if (Change != null)
        {
            Change(deck.Count);
        }
    }
}