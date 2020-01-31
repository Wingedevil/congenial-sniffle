using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChangeScrapBonusEFfect", menuName = "ScriptableObjects/ChangeScrapBonusEFfect", order = 1)]
public class ChangeScrapBonusEffect : Effect {
    public int ChangeWoodScrapBonus;
    public int ChangeSteelScrapBonus;
    public int ChangeGoldScrapBonus;
    public override void ApplyEffect() {
        GameLogicManager.Instance.IncreaseWoodScrapBonus(ChangeWoodScrapBonus);
        GameLogicManager.Instance.IncreaseSteelScrapBonus(ChangeSteelScrapBonus);
        GameLogicManager.Instance.IncreaseGoldScrapBonus(ChangeGoldScrapBonus);
    }
}
