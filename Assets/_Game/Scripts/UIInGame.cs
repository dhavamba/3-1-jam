using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    private Card newCard;
    private Dictionary<ValueCard, Sprite> spriteCard;
    private Image newCardPosition;
    private List<Transform> myHand;
    private int cardInHand;
    private Card[] cards;

    [SerializeField]
    private Sprite[] sprites;

    private void Awake()
    {
        cardInHand = 0;
        cards = new Card[3];
        newCardPosition = transform.Find("NewCard").GetComponent<Image>();
        myHand = new List<Transform>();
        Transform aux = transform.Find("MyHand");
        myHand.Add(aux.GetChild(0));
        myHand.Add(aux.GetChild(1));
        myHand.Add(aux.GetChild(2));

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

    private void NewCard(Card c)
    {
        if (c?.value != null)
        {
            if (newCard?.value != null)
            {
                Deck.Instance<Deck>().InsertTail(c);
            }
            newCard = c;
            newCardPosition.sprite = spriteCard[newCard.value];
            newCardPosition.gameObject.SetActive(true);
        }
    }

    public void RemoveCard()
    {
        if (cardInHand < 3)
        {
            newCardPosition.gameObject.SetActive(false);
            int aux = ReturnFirstDiponible();
            cards[aux] = newCard;
            myHand[aux].gameObject.SetActive(true);
            myHand[aux].GetComponent<Image>().sprite = spriteCard[newCard.value];
            newCard = null;
            cardInHand++;
        }
    }

    public void RemoveCardMyHand(int i)
    {
        Deck.Instance<Deck>().InsertTail(cards[i]);
        cards[i] = null;
        myHand[i].gameObject.SetActive(false);
        cardInHand--;
    }

    private int ReturnFirstDiponible()
    {
        for (int i = 0; i < myHand.Count; i++)
        {
            if (!myHand[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1;
    }
}
