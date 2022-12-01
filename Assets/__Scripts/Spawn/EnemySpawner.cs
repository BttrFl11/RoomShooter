using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

namespace EnemySpawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Wave[] _waves;
        [SerializeField] private string _spawnPointTag = "SpawnPoint";
        [SerializeField] private float _timeBtwWaves;

        private List<Transform> _spawnPoints = new();
        private Wave _currentWave;
        private int _enemiesAlive;
        private bool _wavesEnded;

        public static Action OnEnemiesDestroyed;

        public int EnemiesAlive
        {
            get => _enemiesAlive;
            private set
            {
                _enemiesAlive = value;

                if (_enemiesAlive == 0 && _wavesEnded)
                {
                    OnEnemiesDestroyed?.Invoke();
                    Debug.Log("WIN");
                }
            }
        }

        private void Awake()
        {
            FindSpawnPoints();
        }

        private void FindSpawnPoints()
        {
            var points = GameObject.FindGameObjectsWithTag(_spawnPointTag);
            foreach (var point in points)
                _spawnPoints.Add(point.transform);
        }

        private void Start()
        {
            StartCoroutine(StartWaves());
        }

        private IEnumerator StartWaves()
        {
            for (int i = 0; i < _waves.Length; i++)
            {
                _currentWave = _waves[i];
                float timeBtwSpawns = 1 / _currentWave.SpawnRate;

                yield return new WaitForSeconds(_timeBtwWaves);
                yield return new WaitForSeconds(_currentWave.StartDelay);

                for (int j = 0; j < _currentWave.EnemyCount; j++)
                {
                    SpawnEnemy(_currentWave.EnemyPrefab);

                    yield return new WaitForSeconds(timeBtwSpawns);
                }
            }

            _wavesEnded = true;
        }

        private void OnEnemyDied()
        {
            EnemiesAlive--;
        }

        private void SpawnEnemy(Enemy enemyPrefab)
        {
            var randSpawnPoint = _spawnPoints[Range(0, _spawnPoints.Count)];
            var enemy = Instantiate(enemyPrefab, randSpawnPoint.position, randSpawnPoint.rotation);
            EnemiesAlive++;
            enemy.OnEnemyDied += OnEnemyDied;
        }
    }
}