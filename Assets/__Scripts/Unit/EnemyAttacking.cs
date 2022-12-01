using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(EnemyMomement))]
public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _waitTime;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _attackPoint;

    private float _startTimeBtwAttacks;
    private float _timeBtwAttacks;
    private EnemyMomement _movement;
    private Animator _animator;
    private bool _isAttacking;

    private bool CanAttack => _timeBtwAttacks <= 0 && _isAttacking == false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyMomement>();

        _startTimeBtwAttacks = 1 / _attackRate;
    }

    private void Update()
    {
        _movement.CanMove = !TargetInRange();

        if (CanAttack)
        {
            if (TargetInRange() == true)
                StartCoroutine(PlayAttackAnimation());
        }
    }

    private void FixedUpdate()
    {
        _timeBtwAttacks -= Time.fixedDeltaTime;
    }

    private bool TargetInRange()
    {
        var colls = Physics.OverlapSphere(_attackPoint.position, _attackRadius, _targetLayer);
        if (colls.Length != 0)
            return true;

        return false;
    }

    private IEnumerator PlayAttackAnimation()
    {
        _isAttacking = true;
        _movement.CanMove = false;

        yield return new WaitForSeconds(_waitTime);

        if (TargetInRange() == false)
            yield return null;

        _animator.SetTrigger("Attack");
    }

    public void Attack()
    {
        var colls = Physics.OverlapSphere(_attackPoint.position, _attackRadius, _targetLayer);
        foreach (var coll in colls)
        {
            if (coll.TryGetComponent(out PlayerStats player))
            {
                player.TakeDamage(_damage);
                break;
            }
        }

        _isAttacking = false;
        _movement.CanMove = true;
        _timeBtwAttacks = _startTimeBtwAttacks;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}