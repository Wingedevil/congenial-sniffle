using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hut", menuName = "ScriptableObjects/Hut", order = 1)]
public class Hut : Objective
{
    private void Awake() {
        Scrappable = false;
    }

    public override void NotUnityUpdate() {
        base.NotUnityUpdate();
        RepairWoodCost = 5 + 4 * GameLogicManager.Instance.HutsBuilt;
    }
}
