using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;

    protected float _health;
    public virtual float Health 
    {
        get => _health;
        protected set
        {
            _health = value;

            if (_health <= 0)
                Die();
        }
    }

    protected virtual void Awake()
    {
        Health = _maxHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
