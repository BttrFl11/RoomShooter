using System;
using UnityEngine;

[RequireComponent(typeof(EnemyStats), typeof(EnemyMomement))]
public class Enemy : MonoBehaviour
{
    public Action OnEnemyDied;
}
