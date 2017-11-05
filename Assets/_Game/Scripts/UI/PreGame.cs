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
    void Start ()
    {
        numberText = GameObject.Find("Number").GetComponent<Text>();
        GameObject.Find("Max").GetComponent<Text>().text = Deck.Instance<Deck>().maxDeck + "";
    }

    private void ChangeNumber(int n)
    {
        numberText.text = n + "";
    }
}
