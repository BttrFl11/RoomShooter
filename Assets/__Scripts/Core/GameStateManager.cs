using EnemySpawn;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _completeEvent;
    [SerializeField] private UnityEvent _loseEvent;

    private bool _changeableState = true;
    private GameState _currentState;
    public GameState CurrentState
    {
        get => _currentState;
        private set
        {
            if (_changeableState == false)
                return;

            _currentState = value;

            switch (CurrentState)
            {
                case GameState.Complete:
                    Complete();
                    break;
                case GameState.Lose:
                    Lose();
                    break;
                case GameState.Play:
                    Debug.Log("PLAY");
                    break;
            }
        }
    }

    public static Action OnComplete;
    public static Action OnLose;

    private void Awake()
    {
        CurrentState = GameState.Play;
    }

    private void OnEnable()
    {
        PlayerStats.OnDied += OnPlayerDied;
        EnemySpawner.OnEnemiesDestroyed += OnAllEnemiesDestroyed;

    }

    private void OnDisable()
    {
        PlayerStats.OnDied -= OnPlayerDied;
        EnemySpawner.OnEnemiesDestroyed -= OnAllEnemiesDestroyed;

    }

    private void OnPlayerDied()
    {
        CurrentState = GameState.Lose;
    }

    private void OnAllEnemiesDestroyed()
    {
        CurrentState = GameState.Complete;
    }

    private void Complete()
    {
        _changeableState = false;

        OnComplete?.Invoke();
        _completeEvent?.Invoke();
    }
    private void Lose()
    {
        _changeableState = false;

        OnLose?.Invoke();
        _loseEvent?.Invoke();
    }
}

public enum GameState
{
    Complete,
    Lose,
    Play
}
