using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public GameObject CardPrefab;
    public Transform RiverStart;
    public Transform StorageStart;

    public List<GameObject> RiverObjects;
    public List<GameObject> AssetObjects;

    public enum ClickType
    {
        REPAIR,
        SCRAP
    }
    public ClickType CurrentClickType = ClickType.REPAIR;
    public void DrawRiver()
    {
        foreach (GameObject go in RiverObjects)
        {
            Destroy(go);
        }
        RiverObjects.Clear();

        int i = 0;
        foreach (Card card in GameLogicManager.Instance.PlayerRiver)
        {
            if (card == null)
            {
                ++i;
                continue;
            }
            GameObject go = GameObject.Instantiate(CardPrefab, RiverStart.position + Vector3.right * 1.3f * i, Quaternion.identity, transform);
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
            GameObject go = GameObject.Instantiate(CardPrefab, StorageStart.position + Vector3.right * 1.3f * i, Quaternion.identity, transform);
            AssetObjects.Add(go);
            UICard uiCard = go.GetComponent<UICard>();
            uiCard.CardData = card;
            uiCard.UpdateCardData();
            uiCard.enabled = false;
            ++i;
        }
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
        }
    }
}
