using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool _dontDestroyOnLoad;

    private GameInput _gameInput;

    private Vector2 _moveDirection;
    public Vector2 MoveDirection
    {
        get => _moveDirection;
    }

    public bool GetShootInput
    {
        get => _gameInput.Gameplay.Shooting.IsPressed();
    }

    private bool _readInput = true;
    public bool ReadInput
    {
        get => _readInput;
        set
        {
            _readInput = value;

            if (_readInput == true)
                _gameInput.Enable();
            else
                _gameInput.Disable();
        }
    }

    private static InputManager _instance;
    public static InputManager Instance { get => _instance; }


    private void Awake()
    {
        Initialize();

        CreateNewGameInput();
    }

    private void Initialize()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple InputManagers detected in the scene!");

            Destroy(gameObject);
        }
    }

    private void CreateNewGameInput()
    {
        _gameInput = new GameInput();

        ReadInput = true;
    }

    private void Update()
    {
        if (ReadInput == false)
            return;

        ReadMoveInput();
    }

    private void ReadMoveInput()
    {
        _moveDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        _moveDirection.Normalize();
    }
}