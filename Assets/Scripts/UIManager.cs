using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Manager<UIManager>
{
    public GameObject CardPrefab;
    public Transform RiverStart;
    public Transform StorageStart;
    public TextMeshProUGUI ResourcesText;

    public List<GameObject> RiverObjects;
    public List<GameObject> AssetObjects;

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

        int i = 0;
        List<Card> cardsInBottomRow = IsRiverShown ? GameLogicManager.Instance.PlayerRiver : GameLogicManager.Instance.PlayerObjectives;
        foreach (Card card in cardsInBottomRow)
        {
            if (card == null)
            {
                ++i;
                continue;
            }
            GameObject go = GameObject.Instantiate(CardPrefab, RiverStart.position + Vector3.right * 1.8f * i, Quaternion.identity, transform);
            RiverObjects.Add(go);
            UICard uiCard = go.GetComponent<UICard>();
            uiCard.CardData = card;
            uiCard.UpdateCardData();
            ++i;
        }
    }

    public void DrawAssets()
    {
        foreach (GameObject go in AssetObjects)
        {
            Destroy(go);
        }
        AssetObjects.Clear();

        int i = 0;
        foreach (Card card in GameLogicManager.Instance.PlayerAssets)
        {
            if (card == null)
            {
                ++i;
                continue;
            }
            GameObject go = GameObject.Instantiate(CardPrefab, StorageStart.position + Vector3.right * 1.8f * i, Quaternion.identity, transform);
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
        ResourcesText.text = string.Format("W: {0}, S: {1}, G: {2}, Actions: {3}, River Size: {4}, Pop: {5}, Happy: {8} Turn: {6}, Click: {7}, Bottom Row: {9}",
            resources.Wood,
            resources.Steel,
            resources.Gold,
            resources.Actions,
            resources.RiverSize,
            resources.Population,
            GameLoopManager.Instance.Turn,
            UIManager.Instance.CurrentClickType,
            resources.Happiness,
            IsRiverShown ? "River" : "Objectives"
        );
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
