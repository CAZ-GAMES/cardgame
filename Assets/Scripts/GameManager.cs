using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.ShuffleUpAndDeal);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.ShuffleUpAndDeal:
                break;
            case GameState.PlayerReadyUp:
                break;
            case GameState.Ready:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.ComputerTurn:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    ShuffleUpAndDeal,
    PlayerReadyUp,
    Ready,
    PlayerTurn,
    ComputerTurn,
    Win,
    Lose,
}