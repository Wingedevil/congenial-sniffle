using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject {
    public int WoodCost;
    public int SteelCost;
    public int GoldCost;
    public int ActionCost;

    public virtual void ApplyEffect() {

    }
}
