using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveAsset : MonoBehaviour, IPointerClickHandler
{
    public bool MoveLeft = false;
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("here");
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
            if (ui.CurrentAssetIndex + 6 <= gm.PlayerAssets.Count)
            {
                ui.CurrentAssetIndex += 6;
            }
        }
        ui.DrawAssets();
    }
}
