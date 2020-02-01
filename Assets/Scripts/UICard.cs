using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler
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

    private SpriteRenderer sprite;

    public bool IsInAssets = false;
    public Vector3 OriginalPosition;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCardData();
    }
    public void UpdateCardData()
    {
        CardData.NotUnityUpdate();
        CardImage.sprite = CardData.Image == null ? DefaultSprite : CardData.Image;
        RepairWoodCost.text = CardData.RepairWoodCost.ToString();
        RepairSteelCost.text = CardData.RepairSteelCost.ToString();
        RepairGoldCost.text = CardData.RepairGoldCost.ToString();
        if (CardData.Scrappable)
        {
            ScrapWoodCost.text = CardData.ScrapWoodCost.ToString();
            ScrapSteelCost.text = CardData.ScrapSteelCost.ToString();
            ScrapGoldCost.text = CardData.ScrapGoldCost.ToString();
        }
        else
        {
            ScrapWoodCost.text = "-";
            ScrapSteelCost.text = "-";
            ScrapGoldCost.text = "-";
        }
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
                    return;
            }
             if (h.gameObject.tag == "Trash")
            {
                if (GameLogicManager.Instance.Scrap(CardData))
                    return;
            }
        }
        transform.position = OriginalPosition;
        UIManager.Instance.ToggleRepairDrop(false);
        UIManager.Instance.ToggleTrashDrop(false);
    }

    public void OnDrag(PointerEventData e)
    {
        Vector3 pos = transform.position;
        pos.x = Camera.main.ScreenToWorldPoint(e.position).x;
        pos.y = Camera.main.ScreenToWorldPoint(e.position).y;
        pos.z = -1.1f;
        transform.position = pos;
        bool canRepair = GameLogicManager.Instance.CanRepair(CardData);
        UIManager.Instance.ToggleRepairDrop(canRepair);
        UIManager.Instance.ToggleTrashDrop(true);
    }

    public void OnPointerClick(PointerEventData data)
    {
        // if (!IsInAssets)
        // {
        //     if (UIManager.Instance.CurrentClickType == UIManager.ClickType.REPAIR)
        //     {
        //         GameLogicManager.Instance.TryRepair(CardData);
        //     }
        //     else
        //     {
        //         if (CardData.Scrappable)
        //         {
        //             GameLogicManager.Instance.Scrap(CardData);
        //         }
        //     }
        // } else
        if (!CardData.IsTapped && CardData.OnAction.Length > 0)
        {
            if (GameLogicManager.Instance.TapCard(CardData))
            {
                transform.Rotate(0, 0, 90, Space.Self);
            }
        }
    }
}
