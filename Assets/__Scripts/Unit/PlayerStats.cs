using UnityEngine;
using TMPro;
using System;

public class PlayerStats : Damageable
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _healthText;

    public float MaxHealth => _maxHealth;

    public static Action<PlayerStats> OnHit;
    public static Action OnDied;

    public override float Health 
    {
        get => _health;
        protected set
        {
            base.Health = value;
            _healthText.text = Health.ToString("0");
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        OnHit?.Invoke(this);
    }

    protected override void Die()
    {
        OnDied?.Invoke();

        Destroy(gameObject);
    }
}
