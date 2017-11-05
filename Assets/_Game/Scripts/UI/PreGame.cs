using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGame : MonoBehaviour
{
    Text numberText;

    private void Awake()
    {
        Deck.Change += ChangeNumber;
    }

    private void OnDestroy()
    {
        Deck.Change -= ChangeNumber;
    }

    // Use this for initialization
    private void Start ()
    {
        numberText = GameObject.Find("Number").GetComponent<Text>();
        GameObject.Find("Max").GetComponent<Text>().text = Deck.Instance<Deck>().maxDeck + "";

        Transform cards = GameObject.Find("Cards").transform;
        foreach (Transform imageCard in cards)
        {
            Card card = imageCard.GetComponent<Card>();
            imageCard.Find("Add").GetComponent<Button>().onClick.AddListener(delegate { Deck.Instance<Deck>().AddDeck(card); });
            imageCard.Find("Remove").GetComponent<Button>().onClick.AddListener(delegate { Deck.Instance<Deck>().RemoveDeck(card); });
        }
    }

    private void ChangeNumber(int n)
    {
        numberText.text = n + "";
    }

    public void OnStart()
    {
        if (Deck.Instance<Deck>().IsFull())
        {
            Debug.Log("Start");
        }
    }
}
