        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Card CardData;
    public Sprite DefaultSprite;
    public SpriteRenderer CardImage;
    public TextMeshProUGUI RepairWoodCost;
    public TextMeshProUGUI RepairSteelCost;
    public TextMeshProUGUI RepairGoldCost;
    public TextMeshProUGUI ScrapWoodCost;
    public TextMeshProUGUI ScrapSteelCost;
    public TextMeshProUGUI ScrapGoldCost;
    public TextMeshProUGUI PopCost;

    private SpriteRenderer sprite;

    public bool IsInAssets = false;
    public Vector3 OriginalPosition;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = transform.position;
        UpdateCardData();
    }
    public void UpdateCardData()
    {
        CardData.NotUnityUpdate();
        CardImage.sprite = CardData.Image == null ? DefaultSprite : CardData.Image;
        if (IsInAssets) return;
        var up = GameLogicManager.Instance.PlayerUpgrades;
        RepairWoodCost.text = (CardData.RepairWoodCost + up.WoodCostReduction).ToString();
        RepairSteelCost.text = (CardData.RepairSteelCost+ up.SteelCostReduction).ToString();
        RepairGoldCost.text = (CardData.RepairGoldCost + up.GoldCostReduction).ToString();
        PopCost.text = CardData.PopulationRequirement > 0 ? CardData.PopulationRequirement.ToString() : "";
        if (CardData.Scrappable)
        {
            ScrapWoodCost.text = (CardData.ScrapWoodCost + up.WoodScrapBonus).ToString();
            ScrapSteelCost.text = (CardData.ScrapSteelCost + up.SteelScrapBonus).ToString();
            ScrapGoldCost.text = (CardData.ScrapGoldCost + up.GoldScrapBonus).ToString();
        }
        else
        {
            ScrapWoodCost.text = "-";
            ScrapSteelCost.text = "-";
            ScrapGoldCost.text = "-";
        }
        transform.parent = UIManager.Instance.gameObject.transform;
        OriginalPosition = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hits);
        foreach (RaycastResult h in hits)
        {
            if (h.gameObject.tag == "Drop")
            {
                if (GameLogicManager.Instance.TryRepair(CardData))
                {
                    return;
                }
            }
            if (h.gameObject.tag == "Trash")
            {
                Vector3 pos = transform.position;
                pos.x = Camera.main.ScreenToWorldPoint(eventData.position).x;
                pos.y = Camera.main.ScreenToWorldPoint(eventData.position).y;
                pos.z = -2f;
                if (GameLogicManager.Instance.Scrap(CardData, pos))
                {
                    return;
                }
            }
        }
        transform.position = OriginalPosition;
        transform.parent = UIManager.Instance.gameObject.transform;
        UIManager.Instance.ToggleRepairDrop(false);
        UIManager.Instance.ToggleTrashDrop(false);
        UIManager.Instance.DisableHovered();
    }

    public void OnDrag(PointerEventData e)
    {
        if (IsInAssets) return;
        Vector3 pos = transform.position;
        pos.x = Camera.main.ScreenToWorldPoint(e.position).x;
        pos.y = Camera.main.ScreenToWorldPoint(e.position).y;
        pos.z = -2f;
        transform.position = pos;
        bool canRepair = GameLogicManager.Instance.CanRepair(CardData);

        UIManager.Instance.ToggleRepairDrop(canRepair);
        pos = transform.position;
        pos.x += e.position.x > Screen.width / 2.0 ? -3.5f : 3.5f;
        pos.x = Mathf.Clamp(pos.x , -6.25f, 6.17f);
        pos.y = Mathf.Clamp(pos.y, -1.53f, 1.84f);
        UIManager.Instance.DrawHovered(CardData, pos);
        UIManager.Instance.ToggleTrashDrop(CardData.Scrappable);
        transform.parent = UIManager.Instance.CardCanvas.transform;
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (IsInAssets && !CardData.IsTapped && CardData.OnAction.Length > 0)
        {
            GameLogicManager.Instance.TapCard(CardData);
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        Vector3 pos = transform.position;
        pos.x += e.position.x > Screen.width / 2.0 ? -3.5f : 3.5f;
        pos.x = Mathf.Clamp(pos.x , -6.25f, 6.17f);
        pos.y = Mathf.Clamp(pos.y, -1.53f, 1.84f);
        UIManager.Instance.DrawHovered(CardData, pos);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        UIManager.Instance.DisableHovered();
    }
}
