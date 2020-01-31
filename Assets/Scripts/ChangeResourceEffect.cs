using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeResourceEffect", menuName = "ScriptableObjects/ChangeResourceEffect", order = 1)]
public class ChangeResourceEffect : Effect {
    public int ChangeWood;
    public int ChangeSteel;
    public int ChangeGold;
    public int ChangePopulation;
    public int ChangeHappiness;
    public int ChangeRiverSize;
    public int ChangeStorage;
    public override void ApplyEffect() {
        GameLogicManager.Instance.IncreaseWood(ChangeWood);
        GameLogicManager.Instance.IncreaseSteel(ChangeSteel);
        GameLogicManager.Instance.IncreaseGold(ChangeGold);
        GameLogicManager.Instance.IncreasePopulation(ChangePopulation);
        GameLogicManager.Instance.IncreaseHappiness(ChangeHappiness);
        GameLogicManager.Instance.IncreaseRiverSize(ChangeRiverSize);
        GameLogicManager.Instance.IncreaseStorage(ChangeStorage);
    }
}

