using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Singleton<Deck>
{
    public int maxDeck;
    private List<Card>[] deck;

    private void Awake()
    {
        deck = new List<Card>[2];
        deck[0] = new List<Card>();
        deck[1] = new List<Card>();
    }

    public delegate void Action(int number, Card a, bool add);
    public static event Action Change;

    private void RandomDeck(int player)
    {
        List<Card> auxDeck = deck[player];

        for (int i = 0; i < maxDeck; i++)
        {
            int aux = Random.Range(0, deck[player].Count - 1);
            auxDeck.Add(deck[player][aux]);
            deck[player].RemoveAt(aux);
        }

        deck[player] = auxDeck;
    }

    private void Clear(int player)
    {
        deck[player].Clear();
    }

    public Card Pop(int player)
    {
        int tmp = deck[player].Count;
        if (tmp != 0)
        {
            Card aux = deck[player][tmp - 1];
            deck[player].RemoveAt(tmp - 1);
            return aux;
        }
        else
        {
            return null;
        }
    }

    public void InsertTail(int player, Card c)
    {
        deck[player].Insert(0, c);
    }

    public bool IsFull(int player)
    {
        if (deck[player].Count == maxDeck)
        {
            RandomDeck(player);
            return true;
        }
        return false;
    }

    public void AddDeck(int player, Card c)
    {
        if (deck[player].Count < maxDeck)
        {
            deck[player].Add(c);
            CallEvent(player, c, true);
        }
    }

    public void RemoveDeck(int player, Card c)
    {
        int aux = deck[player].FindIndex(x => x.Equals(c));
        if (aux != -1)
        {
            deck[player].RemoveAt(aux);
            CallEvent(player, c, false);
        }
    }

    private void CallEvent(int player, Card c, bool add)
    {
        if (Change != null)
        {
            Change(deck[player].Count, c, add);
        }
    }
}