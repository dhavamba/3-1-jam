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
        List<Card> auxDeck = deck;

        for (int i = 0; i < maxDeck; i++)
        {
            int aux = Random.Range(0, deck.Count - 1);
            auxDeck.Add(deck[aux]);
            deck.RemoveAt(aux);
        }

        deck = auxDeck;
    }

    public bool IsFull()
    {
        if (deck.Count == maxDeck)
        {
            RandomDeck();
            return true;
        }
        return false;
    }

    public void AddDeck(Card c)
    {
        if (deck.Count < maxDeck)
        {
            deck.Add(c);
            CallEvent();
        }
    }

    public void RemoveDeck(Card c)
    {
        int aux = deck.FindIndex(x => x.Equals(c));
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