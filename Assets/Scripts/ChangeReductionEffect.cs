using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChangeReductionEffect", menuName = "ScriptableObjects/ChangeReductionEffect", order = 1)]
public class ChangeReductionEffect : Effect {
    public int ChangeWoodReduction;
    public int ChangeSteelReduction;
    public int ChangeGoldReduction;
    public override void ApplyEffect() {
        GameLogicManager.Instance.IncreaseWoodCostReduction(ChangeWoodReduction);
        GameLogicManager.Instance.IncreaseSteelCostReduction(ChangeSteelReduction);
        GameLogicManager.Instance.IncreaseGoldCostReduction(ChangeGoldReduction);
    }
}