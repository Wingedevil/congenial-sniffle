using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System;

public class GameManager : Manager<GameManager>
{
    public const string Tag = "GameManager";

    public enum GameState
    {
        Pregame,
        Running,
        Paused,
        Postgame
    }


    public GameState CurrentGameState { get; set; } = GameState.Pregame;

    private void Start()
    {
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Pregame)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;

        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (CurrentGameState)
        {
            case GameState.Pregame:
                Time.timeScale = 1.0f;
                break;
            case GameState.Running:
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                Time.timeScale = 0.0f;
                break;
        }
    }

    protected void OnDestroy()
    {
    }

    public void StartGame()
    {
        GameLoopManager.Instance.OnGameStart();
        UpdateState(GameState.Running);
    }

    public void TogglePause()
    {
        UpdateState(CurrentGameState == GameState.Running ? GameState.Paused : GameState.Running);
    }

    public void RestartGame()
    {
        UpdateState(GameState.Pregame);
    }

    public static void QuitGame()
    {
        // implement features for quitting
        Application.Quit();
    }
}
