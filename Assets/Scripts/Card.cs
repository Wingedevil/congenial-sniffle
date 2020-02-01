using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public int ID;
    public int Tier;

    public string Name;
    public Sprite Image;

    public string Description;
    public Effect[] OnRepair;
    public Effect[] OnAction;
    public Effect[] OnEndTurn;

    public int ScrapWoodCost;
    public int ScrapSteelCost;
    public int ScrapGoldCost;

    public int RepairWoodCost;
    public int RepairSteelCost;
    public int RepairGoldCost;

    public bool IsTapped;

    [HideInInspector]
    public bool Scrappable = true;
}
