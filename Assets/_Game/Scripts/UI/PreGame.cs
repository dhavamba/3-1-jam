using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGame : MonoBehaviour
{
    private Text numberText;
    public Dictionary<ValueCard, Text> dict;

    private void Awake()
    {
        dict = new Dictionary<ValueCard, Text>();
        Deck.Change += ChangeNumber;
    }

    private void OnDestroy()
    {
        Deck.Change -= ChangeNumber;
    }

    // Use this for initialization
    private void Start ()
    {
        numberText = GameObject.Find("NumberTotalCard").GetComponent<Text>();
        GameObject.Find("Max").GetComponent<Text>().text = Deck.Instance<Deck>().maxDeck + "";

        Transform cards = GameObject.Find("Cards").transform;
        foreach (Transform imageCard in cards)
        {
            Card card = imageCard.GetComponent<Card>();
            dict.Add(card.value, imageCard.GetComponentInChildren<Text>());
            imageCard.Find("Add").GetComponent<Button>().onClick.AddListener(delegate { Deck.Instance<Deck>().AddDeck(card); });
            imageCard.Find("Remove").GetComponent<Button>().onClick.AddListener(delegate { Deck.Instance<Deck>().RemoveDeck(card); });
        }
    }

    private void ChangeNumber(int n, Card a, bool add)
    {
        numberText.text = n + "";
        if (add)
        {
            dict[a.value].text = Int32.Parse(dict[a.value].text) + 1 + "";
        }
        else
        {
            dict[a.value].text = Int32.Parse(dict[a.value].text) - 1 + "";
        }
    }

    public void OnStart()
    {
        if (Deck.Instance<Deck>().IsFull())
        {
            Debug.Log("Start");
        }
    }
}
