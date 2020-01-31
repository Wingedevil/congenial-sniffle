using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect", order = 1)]
public class Effect : ScriptableObject {
    public int WoodCost;
    public int SteelCost;
    public int GoldCost;
    public int ActionCost;

    protected GameManager glm;
    public Effect() {
        glm = GameObject.FindObjectOfType<GameManager>();
    }
    public virtual void ApplyEffect() {

    }
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChangeResourceEffect", order = 1)]
public class ChangeResourceEffect : Effect {
    public int ChangeWood;
    public int ChangeSteel;
    public int ChangeGold;
    public int ChangeHappiness;
    public int ChangePopulation;
    public int ChangeRiverSize;
    public int ChangeStorage;
    public override void ApplyEffect() {
        // glm.ChangeWood(ChangeWood);
        // glm.ChangeSteel(ChangeSteel);
        // glm.ChangeGold(ChangeGold);
        // glm.ChangeHappiness(ChangeHappiness);
        // glm.ChangePopulation(ChangePopulation);
        // glm.ChangeRiverSize(ChangeRiverSize);
        // glm.ChangeStorage(ChangeStorage);
        // ...
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChangeRiverEffect", order = 1)]
public class ChangeRiverEffect : Effect {
    public bool MoveLeftToRight;
    public bool MoveRightToLeft;
    public int MoveRiverNTimes;
    public override void ApplyEffect() {
        // if(MoveLeftToRight) glm.MoveLeftToRight();
        // if(MoveRightToLeft) glm.MoveRightToLeft();
        // glm.MoveRiverNTimes(MoveRiverNTimes);
        // ...
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChangeReductionEffect", order = 1)]
public class ChangeReductionEffect : Effect {
    public int ChangeWoodReduction;
    public int ChangeSteelReduction;
    public int ChangeGoldReduction;
    public override void ApplyEffect() {
        // glm.ChangeWoodReduction(ChangeWoodReduction);
        // glm.ChangeSteelReduction(ChangeSteelReduction);
        // glm.ChangeSteelReduction(ChangeSteelReduction);
        // ...
    }
}

