using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "ScriptableObjects/Objective", order = 1)]
public class Objective : Card {
    private void Awake() {
        Scrappable = false;
    }
}
