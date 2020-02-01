using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverCard : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite DefaultSprite;
    public SpriteRenderer CardImage;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI RepairWoodCost;
    public TextMeshProUGUI RepairSteelCost;
    public TextMeshProUGUI RepairGoldCost;
    public TextMeshProUGUI ScrapWoodCost;
    public TextMeshProUGUI ScrapSteelCost;
    public TextMeshProUGUI ScrapGoldCost;

    public void UpdateCard(Card card)
    {
        CardImage.sprite = card.Image == null ? DefaultSprite : card.Image;
        Name.text = card.Name;
        Description.text = card.Description;
        RepairWoodCost.text = card.RepairWoodCost.ToString();
        RepairSteelCost.text = card.RepairSteelCost.ToString();
        RepairGoldCost.text = card.RepairGoldCost.ToString();
        if (card.Scrappable)
        {
            ScrapWoodCost.text = card.ScrapWoodCost.ToString();
            ScrapSteelCost.text = card.ScrapSteelCost.ToString();
            ScrapGoldCost.text = card.ScrapGoldCost.ToString();
        }
        else
        {
            ScrapWoodCost.text = "-";
            ScrapSteelCost.text = "-";
            ScrapGoldCost.text = "-";
        }
    }
}
