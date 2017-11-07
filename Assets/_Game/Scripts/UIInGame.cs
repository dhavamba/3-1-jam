using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    private Card newCard;
    private Dictionary<ValueCard, Sprite> spriteCard;
    private Image newCardPosition;
    [SerializeField]
    private Sprite[] sprites;

    private void Awake()
    {
        newCardPosition = transform.Find("NewCard").GetComponent<Image>();
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
            newCard = c;
            newCardPosition.sprite = spriteCard[newCard.value];
            newCardPosition.enabled = true;
            newCardPosition.GetComponent<Button>().enabled = true;
        }
    }
}
