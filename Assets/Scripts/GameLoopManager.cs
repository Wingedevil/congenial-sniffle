﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : Manager<GameLoopManager>
{
    public enum GamePhase
    {
        PREGAME,
        PREINPUT,
        INPUT,
        CLEANUP,
        END
    }

    public GamePhase CurrentPhase = GamePhase.PREGAME;

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        GameLogicManager.Instance.ResetActions();
        for (int i = 0; i < 3; ++i)
        {
            GameLogicManager.Instance.MoveRiver();
        }
        // we can do some delay before this
        CurrentPhase = GamePhase.INPUT;
    }

    void PreInput()
    {
        CurrentPhase = GamePhase.PREINPUT;
        GameLogicManager.Instance.ResetActions();
        GameLogicManager.Instance.MoveRiver();
        // we can do some delay before this
        CurrentPhase = GamePhase.INPUT;
    }

    public void NoMoreActions()
    {
        CleanUp();
    }

    void CleanUp()
    {
        CurrentPhase = GamePhase.CLEANUP;
        GameLogicManager.Instance.DiscardResources();
        GameLogicManager.Instance.UntapCards();
        // we can do some delay before this
        PreInput();
    }
}
