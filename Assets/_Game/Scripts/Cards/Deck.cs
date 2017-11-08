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

    public delegate void Action(int number, Card a, bool add);
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

    private void Clear()
    {
        deck.Clear();
    }

    public Card Pop()
    {
        if (deck.Count != 0)
        {
            Card aux = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return aux;
        }
        else
        {
            return null;
        }
    }

    public void InsertTail(Card c)
    {
        deck.Insert(0, c);
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
            CallEvent(c, true);
        }
    }

    public void RemoveDeck(Card c)
    {
        int aux = deck.FindIndex(x => x.Equals(c));
        if (aux != -1)
        {
            deck.RemoveAt(aux);
            CallEvent(c, false);
        }
    }

    private void CallEvent(Card c, bool add)
    {
        if (Change != null)
        {
            Change(deck.Count, c, add);
        }
    }
}