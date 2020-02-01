using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour, IPointerClickHandler
{
    public Card CardData;
    public Sprite DefaultSprite;
    public SpriteRenderer CardImage;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI RepairCost;
    public TextMeshProUGUI ScrapCost;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCardData();
    }
    public void UpdateCardData()
    {
        CardData.NotUnityUpdate();
        CardImage.sprite = CardData.Image == null ? DefaultSprite : CardData.Image;
        Name.text = CardData.Name;
        Description.text = CardData.Description;
        RepairCost.text = string.Format("W {0}\nS {1}\nG {2}", CardData.RepairWoodCost, CardData.RepairSteelCost, CardData.RepairGoldCost);
        ScrapCost.text = string.Format("W {0}\nS {1}\nG {2}", CardData.ScrapWoodCost, CardData.ScrapSteelCost, CardData.ScrapGoldCost);
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (UIManager.Instance.CurrentClickType == UIManager.ClickType.REPAIR)
        {
            GameLogicManager.Instance.TryRepair(CardData);
        }
        else
        {
            GameLogicManager.Instance.Scrap(CardData);
        }

    }
}
