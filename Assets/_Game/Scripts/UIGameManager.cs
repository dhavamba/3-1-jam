using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGameManager : Singleton<UIInGame>
{
    private Dictionary<ValueCard, Sprite> spriteCard;
    private Sprite[] sprites;
    public Text[] SpeedPlayer;
    public Text[] Score;
    GameManager gm;
    private List<Transform>[] myHand;

    private void Awake()
    {

        myHand = new List<Transform>[2];
        myHand[0] = new List<Transform>();
        myHand[1] = new List<Transform>();
        Transform aux1 = transform.Find("Player 1/MyHand");
        Transform aux2 = transform.Find("Player 2/MyHand");
        for (int i = 0; i < aux1.childCount; i++)
        {

            myHand[0].Add(aux1.GetChild(i));
            aux1.GetChild(i).gameObject.SetActive(false);
            myHand[1].Add(aux2.GetChild(i));
            aux2.GetChild(i).gameObject.SetActive(false);
        }



        gm = GameManager.Instance<GameManager>();

        spriteCard = new Dictionary<ValueCard, Sprite>();

        spriteCard[ValueCard.LateralWind] = sprites[0];
        spriteCard[ValueCard.SlowDown] = sprites[1];
        spriteCard[ValueCard.SpeedUp] = sprites[2];
        spriteCard[ValueCard.Freeze] = sprites[3];
        spriteCard[ValueCard.Invulnerable] = sprites[4];
        spriteCard[ValueCard.Teleport] = sprites[5];
        spriteCard[ValueCard.Pillars] = sprites[6];
        spriteCard[ValueCard.Boulders] = sprites[7];
        spriteCard[ValueCard.Mix] = sprites[8];
        spriteCard[ValueCard.GlassBlocks] = sprites[9];
    }


    private void Start()
    {
        TimeForCard.Change += NewCard;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        TimeForCard.Change -= NewCard;
    }

    private void NewCard(int player, Card c)
    {
        if (c?.value != null)
        {
            Deck.Instance<Deck>().InsertTail(player, c);
            UpdateHand(player, c);
        }
    }

    public void UpdateHand(int player, Card newCard)
    {

        int aux = ReturnFirstDiponible(player);
        myHand[player][aux].gameObject.SetActive(true);
        myHand[player][aux].GetComponent<Image>().sprite = spriteCard[newCard.value];
        
    }

    public void RemoveCardMyHand(int player, int i)
    {
        
            StartCoroutine(EffectWhenRemoveCard(player, i));
        
    }

    IEnumerator EffectWhenRemoveCard(int player, int i)
    {

        yield return new WaitForSeconds(0.35f);
        
        EliminateCard(player, i);

    }


    private void EliminateCard(int player, int i)
    {
       
        
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


    public void Update()
    {
        for (int i = 0; i < SpeedPlayer.Length; i++)
        {
            SpeedPlayer[i].text = gm.getPlayerSpeed(i).ToString("0.00") + "Km/h";
        }

        for (int i = 0; i < SpeedPlayer.Length; i++)
        {
            Score[i].text = "SCORE: " + gm.getPlayerScore(i).ToString();
        }


    }
}
