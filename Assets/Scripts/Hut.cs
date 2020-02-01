using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hut : Card
{
    private void Awake() {
        Scrappable = false;
    }

    public override void NotUnityUpdate() {
        base.NotUnityUpdate();
        RepairWoodCost = 5 + 2 * GameLogicManager.Instance.HutsBuilt;
    }
}
