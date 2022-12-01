using UnityEngine;

public class EnemyStats : Damageable
{
    private Enemy _enemy;

    protected override void Awake()
    {
        base.Awake();

        _enemy = GetComponent<Enemy>();
    }

    protected override void Die()
    {
        _enemy.OnEnemyDied?.Invoke();

        Destroy(gameObject);
    }
}
