using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(AIM), typeof(PlayerStats))]
public class Player : MonoBehaviour
{
    private AIM _aim;
    private PlayerMovement _playerMovement;
    private bool _complete = false;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _aim = GetComponent<AIM>();

        _aim.CanAIM = true;
    }

    private void OnEnable()
    {
        GameStateManager.OnComplete += OnComplete;
    }

    private void OnDisable()
    {
        GameStateManager.OnComplete -= OnComplete;
    }

    private void FixedUpdate()
    {
        if (_complete)
            return;

        Move();
    }

    private void OnComplete()
    {
        InputManager.Instance.ReadInput = false;
        _aim.CanAIM = false;

        _complete = true;
    }

    private void Move()
    {
        var dir = InputManager.Instance.MoveDirection;
        _playerMovement.Move(dir * Time.fixedDeltaTime);
    }
}