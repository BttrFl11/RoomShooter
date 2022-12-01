using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMomement : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _navAgent;

    public bool CanMove
    {
        get => !_navAgent.isStopped;
        set => _navAgent.isStopped = !value;
    }

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _target = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        UpdateTarget();
    }

    private void UpdateTarget()
    {
        _navAgent.destination = _target.position;
    }
}
