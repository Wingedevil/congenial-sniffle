using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveAsset : MonoBehaviour, IPointerClickHandler
{
    public bool MoveLeft = false;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (MoveLeft)
        {
            spriteRenderer.enabled = UIManager.Instance.CurrentAssetIndex > 0;
        }
        else
        {
            spriteRenderer.enabled= UIManager.Instance.CurrentAssetIndex + 6 < GameLogicManager.Instance.PlayerAssets.Count;
        }

    }
    public void OnPointerClick(PointerEventData data)
    {
        var gm = GameLogicManager.Instance;
        var ui = UIManager.Instance;
        if (MoveLeft)
        {
            if (ui.CurrentAssetIndex - 6 >= 0)
            {
                ui.CurrentAssetIndex -= 6;
            }
        }
        else
        {
            if (ui.CurrentAssetIndex + 6 < gm.PlayerAssets.Count)
            {
                ui.CurrentAssetIndex += 6;
            }
        }
        ui.DrawAssets();
    }
}
