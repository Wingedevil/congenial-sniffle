using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectivesHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject Objectives;
    public void OnPointerEnter(PointerEventData e)
    {
        Objectives.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Objectives.SetActive(false);
    }
}
