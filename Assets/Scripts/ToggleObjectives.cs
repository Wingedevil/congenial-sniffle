using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class ToggleObjectives : MonoBehaviour, IPointerClickHandler
{

    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData data)
    {
        UIManager.Instance.IsRiverShown = !UIManager.Instance.IsRiverShown;
        UIManager.Instance.DrawRiver();
        Text.text = UIManager.Instance.IsRiverShown ? "Show Objectives" : "Show River";
    }
}
