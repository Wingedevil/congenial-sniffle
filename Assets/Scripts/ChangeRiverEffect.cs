using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChangeRiverEffect", menuName = "ScriptableObjects/ChangeRiverEffect", order = 1)]
public class ChangeRiverEffect : Effect {
    public bool MoveLeftToRight;
    public bool MoveRightToLeft;
    public int MoveRiverNTimes;
    public override void ApplyEffect() {
        GameLogicManager.Instance.MoveRiver();
    }
}

