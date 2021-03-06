﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIInGame : Singleton<UIInGame>
{
    private Dictionary<ValueCard, Sprite> spriteCard;
    private Image[] newCardPosition;
    private List<Transform>[] myHand;
    private int[] cardInHand;
    private Card[,] cards;
    GameManager gm;
    [SerializeField]
    private Sprite[] sprites;
    public Text[] SpeedPlayer;
    public Text[] Score;
    public GameObject[] Life;
    public  GameObject [] Winners;


    private void Awake()
    {
        cardInHand = new int[2];
        gm = GameManager.Instance<GameManager>();
        cards = new Card[2,3];

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

    public void LostLife(int playerIndex,int life)
    {
        if (life - 1 >= 0)
        {
            Life[playerIndex].transform.GetChild(life).gameObject.SetActive(false);
            Life[playerIndex].transform.GetChild(life - 1).gameObject.SetActive(true);
        }
    }

    public void setWinner(int w,int l)
    {
        
        Winners[w].SetActive(true);
        Winners[l].SetActive(true);
        Winners[w].GetComponentInChildren<Text>().text = "YOU WIN!!";
        Winners[l].GetComponentInChildren<Text>().text = "LOSER!!";

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
            RemoveCard(player, c);
        }
    }

    public void RemoveCard(int player, Card newCard)
    {
        if (cardInHand[player] >= 3)
        {
            EliminateCard(player, 0);
        }
        int aux = ReturnFirstDiponible(player);
        cards[player, aux] = newCard;
        myHand[player][aux].gameObject.SetActive(true);

        myHand[player][aux].GetChild(1).GetComponent<Image>().sprite = spriteCard[newCard.value];
        myHand[player][aux].GetChild(2).GetComponent<Text>().text = newCard.value.ToString();

        //myHand[player][aux].GetComponentInChildren<Image>().sprite = spriteCard[newCard.value];
        newCard = null;
        cardInHand[player]++;
    }

    public void RemoveCardMyHand(int player, int i)
    {
        if (cards[player, i]?.value != null && gm.getPlayerSpeed(player) > cards[player, i].getSpeed())
        {
            StartCoroutine(EffectWhenRemoveCard(player, i));
        }
    }

    IEnumerator EffectWhenRemoveCard(int player, int i)
    {
       
        myHand[player][i].gameObject.transform.localScale = myHand[player][i].gameObject.transform.localScale * 2.0f;
        yield return new WaitForSeconds(0.35f);
        myHand[player][i].gameObject.transform.localScale= myHand[player][i].gameObject.transform.localScale / 2.0f;
        gm.AddDropObstacle(player, cards[player, i].value);
        EliminateCard(player, i);
       
    }


    private void EliminateCard(int player, int i)
    {
        Deck.Instance<Deck>().InsertTail(player, cards[player, i]);
        cards[player, i] = null;
        myHand[player][i].gameObject.SetActive(false);
        cardInHand[player]--;
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
        for(int i=0;i<SpeedPlayer.Length;i++)
        {
            SpeedPlayer[i].text = gm.getPlayerSpeed(i).ToString("0.00")+"Km/h";
        }

        for (int i = 0; i < SpeedPlayer.Length; i++)
        {
            Score[i].text = "SCORE: "+gm.getPlayerScore(i).ToString();
        }

        
    }
}
