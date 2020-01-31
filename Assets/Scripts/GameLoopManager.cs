using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : Manager<GameLoopManager>
{
    public void OnGameStart()
    {
        PreInput();
    }

    void PreInput()
    {
        GameLogicManager.Instance.ResetActions();
        GameLogicManager.Instance.MoveRiver();
    }

    public void NoMoreActions()
    {
        CleanUp();
    }

    void CleanUp()
    {
        GameLogicManager.Instance.DiscardResources();
        GameLogicManager.Instance.UntapCards();
        PreInput();
    }
}
