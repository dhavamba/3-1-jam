using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class UIInGame : MonoBehaviour
{
    private Card newCard;
    private Dictionary<ValueCard, Sprite> spriteCard;
    private Image[] newCardPosition;
    private List<Transform>[] myHand;
    private int cardInHand;
    private Card[,] cards;

    [SerializeField]
    private Sprite[] sprites;

    private void Awake()
    {
        cardInHand = 0;

        cards = new Card[2,3];
        newCardPosition = new Image[2];
        newCardPosition[0] = transform.Find("Player 1/NewCard").GetComponent<Image>();
        newCardPosition[1] = transform.Find("Player 2/NewCard").GetComponent<Image>();

        myHand = new List<Transform>[2];
        myHand[0] = new List<Transform>();
        Transform aux = transform.Find("Player 1/MyHand");
        myHand[0].Add(aux.GetChild(0));
        myHand[0].Add(aux.GetChild(1));
        myHand[0].Add(aux.GetChild(2));
        myHand[1] = new List<Transform>();
        aux = transform.Find("Player 2/MyHand");
        myHand[1].Add(aux.GetChild(0));
        myHand[1].Add(aux.GetChild(1));
        myHand[1].Add(aux.GetChild(2));

        spriteCard = new Dictionary<ValueCard, Sprite>();

        spriteCard[ValueCard.LateralWind] = sprites[0];
        spriteCard[ValueCard.SlowDown] = sprites[1];
        spriteCard[ValueCard.SpeedUp] = sprites[2];
        spriteCard[ValueCard.Freze] = sprites[3];
        spriteCard[ValueCard.Invulnerable] = sprites[4];
        spriteCard[ValueCard.Teleport] = sprites[5];
        spriteCard[ValueCard.Pillars] = sprites[6];
        spriteCard[ValueCard.BouldersAbove] = sprites[7];
        spriteCard[ValueCard.BouldersBelow] = sprites[8];
        spriteCard[ValueCard.Mix] = sprites[9];
        spriteCard[ValueCard.GlassBlocks] = sprites[10];
    }

    private void Start()
    {
        TimeForCard.Change += NewCard;
    }

    private void OnDestroy()
    {
        TimeForCard.Change -= NewCard;
    }

    private void NewCard(int player, Card c)
    {
        if (c?.value != null)
        {
            if (newCard?.value != null)
            {
                Deck.Instance<Deck>().InsertTail(player, c);
            }
            newCard = c;
            newCardPosition[player].sprite = spriteCard[newCard.value];
            newCardPosition[player].gameObject.SetActive(true);
        }
    }

    public void RemoveCard(int player)
    {
        if (cardInHand < 3)
        {
            newCardPosition[player].gameObject.SetActive(false);
            int aux = ReturnFirstDiponible(player);
            cards[player,aux] = newCard;
            myHand[player][aux].gameObject.SetActive(true);
            myHand[player][aux].GetComponent<Image>().sprite = spriteCard[newCard.value];
            newCard = null;
            cardInHand++;
        }
    }

    public void RemoveCardMyHand(string aux)
    {
        int player = Int32.Parse(aux[0] + "");
        int i = Int32.Parse(aux[1] + "");
        Deck.Instance<Deck>().InsertTail(player, cards[player,i]);

        // devo passare l'indice del giocatore giocatore 1 o 0
        GameManager.Instance<GameManager>().AddDropObstacle(1, cards[player,i].value);

        cards[player,i] = null;
        myHand[player][i].gameObject.SetActive(false);
        cardInHand--;
    }

    private int ReturnFirstDiponible(int player)
    {
        for (int i = 0; i < myHand[player].Count; i++)
        {
            if (!myHand[player][i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1;
    }
}
