using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _spreadValue;
    [SerializeField] protected LayerMask _hitLayer;
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected string _attackAnimationName = "Fire";

    protected float _startTimeBtwShots;
    protected float _timeBtwShots;
    protected Animator _animation;

    public float FireRate => _fireRate;
    public bool CanShoot => _timeBtwShots <= 0;

    protected virtual void Awake()
    {
        _animation = GetComponent<Animator>();

        _startTimeBtwShots = 1 / _fireRate;
        _timeBtwShots = _startTimeBtwShots;

        _animation.speed = _fireRate;
    }

    protected virtual void FixedUpdate()
    {
        _timeBtwShots -= Time.fixedDeltaTime;
    }

    public abstract void Attack();
}