using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Singleton<Deck>
{
    public int maxDeck;
    private List<Card> aux;
    private Stack<Card> deck;

    public delegate void Action(int number, Card a, bool add);
    public static event Action Change;

    private void Awake()
    {
        aux = new List<Card>();
        deck = new Stack<Card>();
    }

    private void RandomDeck()
    {
        List<Card> auxDeck = aux;

        for (int i = 0; i < maxDeck; i++)
        {
            int aux = Random.Range(0, this.aux.Count - 1);
            auxDeck.Add(this.aux[aux]);
            this.aux.RemoveAt(aux);
        }

        deck = new Stack<Card>(auxDeck);
        aux.Clear();
        auxDeck.Clear();
    }

    private void Clear()
    {
        aux.Clear();
    }

    public Card Pop()
    {
        if (deck.Count == 0)
        {
            return null;
        }
        return deck.Pop();
    }

    public bool IsFull()
    {
        if (aux.Count == maxDeck)
        {
            RandomDeck();
            return true;
        }
        return false;
    }

    public void AddDeck(Card c)
    {
        if (aux.Count < maxDeck)
        {
            aux.Add(c);
            CallEvent(c, true);
        }
    }

    public void RemoveDeck(Card c)
    {
        int aux = this.aux.FindIndex(x => x.Equals(c));
        if (aux != -1)
        {
            this.aux.RemoveAt(aux);
            CallEvent(c, false);
        }
    }

    private void CallEvent(Card c, bool add)
    {
        if (Change != null)
        {
            Change(aux.Count, c, add);
        }
    }
}