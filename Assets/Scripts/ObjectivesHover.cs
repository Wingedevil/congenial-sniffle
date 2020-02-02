using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class ObjectivesHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI UpkeepText;
    public GameObject Objectives;
    public void OnPointerEnter(PointerEventData e)
    {
        int Upkeep = Mathf.FloorToInt(GameLogicManager.HAPPINESS_PER_POPULATION * GameLogicManager.Instance.PlayerResources.Population * -1);
        UpkeepText.text = string.Format("Upkeep: {0} per turn", Upkeep);
        Objectives.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Objectives.SetActive(false);
    }
}
