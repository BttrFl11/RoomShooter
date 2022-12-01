using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private WeaponTracers _tracersPrefab;

    private WeaponTracers _tracers;

    private void OnEnable()
    {
        _tracers = Instantiate(_tracersPrefab, Vector3.zero, Quaternion.identity);
    }

    private void OnDisable()
    {
        Destroy(_tracers);
    }

    private Vector3 GetSpread(Vector3 start, float spreadValue)
    {
        var x = Random.Range(spreadValue, -spreadValue);
        var z = Random.Range(spreadValue, -spreadValue);
        start = new Vector3(x + start.x, start.y, z + start.z);
        return start.normalized;
    }

    private void ShowTracer(RaycastHit hit)
    {
        _tracers.Next(_firePoint.position, hit);
    }

    private void Raycast()
    {
        if (Physics.Raycast(_firePoint.position, GetSpread(_firePoint.forward, _spreadValue), out RaycastHit hit, Mathf.Infinity, _hitLayer))
        {
            if (hit.transform.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(_damage);
            }

            ShowTracer(hit);
        }
    }

    private void Animate()
    {
        _animation.SetTrigger("Fire");
    }

    public override void Attack()
    {
        Raycast();
        Animate();

        _timeBtwShots = _startTimeBtwShots;
    }
}