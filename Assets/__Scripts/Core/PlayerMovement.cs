using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody _rigidbody;
    private Animator _animation;
    private bool _isMoving;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude != 0)
            _rigidbody.velocity = Vector3.zero;
    }

    public void Move(Vector2 dir)
    {
        _isMoving = dir.magnitude > 0;

        var direction = new Vector3(dir.x, 0, dir.y);
        _rigidbody.MovePosition(_rigidbody.position + _moveSpeed * direction);
        _animation.SetBool("isMoving", _isMoving);
    }
}
