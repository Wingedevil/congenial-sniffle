using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class UIManager : Manager<UIManager>
{
    public GameObject CardPrefab;
    public GameObject AssetPrefab;
    public Transform RiverStart;
    public Transform RiverEnd;
    public Transform StorageStart;
    public Transform StorageEnd;
    public TextMeshProUGUI ResourcesText;
    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI SteelText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI TurnText;
    public TextMeshProUGUI PopText;
    public TextMeshProUGUI HappyText;

    public HoverCard Hover;

    public GameObject CardCanvas;


    public List<GameObject> RiverObjects;
    public List<GameObject> AssetObjects;
    public GameObject RepairDrop;
    public GameObject TrashDrop;

    public int CurrentAssetIndex = 0;

    public enum ClickType
    {
        REPAIR,
        SCRAP
    }

    public bool IsRiverShown = true;
    public ClickType CurrentClickType = ClickType.REPAIR;

    public void DrawRiver()
    {
        foreach (GameObject go in RiverObjects)
        {
            Destroy(go);
        }
        RiverObjects.Clear();
        float space = (RiverEnd.position.x - RiverStart.position.x) / 6.0f;
        int i = 0;
        List<Card> cardsInBottomRow = IsRiverShown ? GameLogicManager.Instance.PlayerRiver : GameLogicManager.Instance.PlayerObjectives;
        foreach (Card card in cardsInBottomRow)
        {
            if (card == null)
            {
                ++i;
                continue;
            }
            GameObject go = GameObject.Instantiate(CardPrefab, RiverStart.position + Vector3.right * space * i, Quaternion.identity, transform);
            RiverObjects.Add(go);
            UICard uiCard = go.GetComponent<UICard>();
            uiCard.CardData = card;
            uiCard.UpdateCardData();
            ++i;
        }
    }

    public void ToggleRepairDrop(bool toggle)
    {
        RepairDrop.SetActive(toggle);
    }

    public void ToggleTrashDrop(bool toggle)
    {
        TrashDrop.SetActive(toggle);
    }


    public void DrawAssets()
    {
        foreach (GameObject go in AssetObjects)
        {
            Destroy(go);
        }
        AssetObjects.Clear();
        float space = (StorageEnd.position.x - StorageStart.position.x) / 5.0f;
        int i = 0;
        foreach (Card card in GameLogicManager.Instance.PlayerAssets.Skip(CurrentAssetIndex).Take(6))
        {
            if (card == null)
            {
                ++i;
                continue;
            }
            GameObject go = GameObject.Instantiate(AssetPrefab, StorageStart.position + Vector3.right * space * i, Quaternion.identity, transform);
            AssetObjects.Add(go);
            UICard uiCard = go.GetComponent<UICard>();
            uiCard.CardData = card;
            uiCard.UpdateCardData();
            uiCard.IsInAssets = true;
            ++i;
        }
    }

    public void UpdateResources()
    {
        Resources resources = GameLogicManager.Instance.PlayerResources;
        WoodText.text = resources.Wood.ToString() + "/" + resources.Storage.ToString();
        SteelText.text = resources.Steel.ToString() + "/" + resources.Storage.ToString();
        GoldText.text = resources.Gold.ToString() + "/" + resources.Storage.ToString();
        TurnText.text = "Turn: " + GameLoopManager.Instance.Turn.ToString();
        PopText.text = /*(resources.Population - resources.PopulationAtWork).ToString() + "/" + */resources.Population.ToString();
        HappyText.text = resources.Happiness.ToString();
        // ResourcesText.text = string.Format("W: {0}, S: {1}, G: {2}, Actions: {3}, River Size: {4}, Pop: {5}, Happy: {8} Turn: {6}, Click: {7}, Bottom Row: {9}",
        //     resources.Wood,
        //     resources.Steel,
        //     resources.Gold,
        //     resources.Actions,
        //     resources.RiverSize,
        //     resources.Population,
        //     GameLoopManager.Instance.Turn,
        //     UIManager.Instance.CurrentClickType,
        //     resources.Happiness,
        //     IsRiverShown ? "River" : "Objectives"
        // );
    }

    public void DrawHovered(Card card, Vector3 pos)
    {
        Hover.gameObject.SetActive(true);
        Hover.gameObject.transform.position = pos;
        Hover.UpdateCard(card);
    }

    public void DisableHovered()
    {
        Hover.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameLoopManager.Instance.NoMoreActions();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (CurrentClickType == ClickType.REPAIR)
            {
                CurrentClickType = ClickType.SCRAP;
            }
            else
            {
                CurrentClickType = ClickType.REPAIR;
            }
            Debug.Log(CurrentClickType);
            UpdateResources();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            IsRiverShown = !IsRiverShown;
            DrawRiver();
            UpdateResources();
        }

    }
}
