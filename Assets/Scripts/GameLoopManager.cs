using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{

    void PreInput()
    {
        GameLogicManager.Instance.ResetActions();
        GameLogicManager.Instance.MoveRiver();
    }
}
