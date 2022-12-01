using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private PickableItemSO[] _itemsSO;
    [SerializeField] private PickableItem _pickablePrefab;
    [SerializeField] private Vector2 _spawnTime;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        float time = Random.Range(_spawnTime.x, _spawnTime.y);
        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        var item = _itemsSO[Random.Range(0, _itemsSO.Length)];

        yield return new WaitForSeconds(time);

        var pickable = Instantiate(_pickablePrefab, spawnPoint.position, Quaternion.identity);
        pickable.Initialize(item);

        yield return new WaitUntil(() => pickable == null);

        StartCoroutine(StartSpawn());
    }   
}
