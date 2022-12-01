using UnityEngine;
using System.Collections;

public class WeaponTracers : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHitPrefab;
    [SerializeField] private GameObject _bulletHitDamageablePrefab;
    [SerializeField] private GameObject[] _bloodTrailPrefabs;
    [SerializeField] private int _damageableLayer;
    [SerializeField] private float _speed;

    private TrailRenderer[] _trails;
    private int _index;

    private void Awake()
    {
        _trails = GetComponentsInChildren<TrailRenderer>();

        foreach (var trail in _trails)
            SetTrailActive(trail, false);
    }

    private IEnumerator StartTrail(TrailRenderer trail, Vector3 startPoint, RaycastHit hit)
    {
        SetTrailActive(trail, true);

        trail.transform.position = startPoint;
        Vector3 startPos = trail.transform.position;
        float distance = Vector3.Distance(hit.point, startPoint);
        float startDist = distance;

        while(distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, 1 - (distance / startDist));
            distance -= Time.deltaTime * _speed;

            yield return null;
        }

        SpawnEffect(hit);

        yield return new WaitForSecondsRealtime(Time.deltaTime);
        SetTrailActive(trail, false);
    }

    private void SpawnEffect(RaycastHit hit)
    {
        try
        {
            bool damageable = hit.transform.gameObject.layer == _damageableLayer;
            var hitPrefab =  damageable ? _bulletHitDamageablePrefab : _bulletHitPrefab;
            var effect = Instantiate(hitPrefab, hit.point - new Vector3(0, 0, 0.01f), Quaternion.LookRotation(hit.normal));
            effect.transform.SetParent(hit.transform);
            Destroy(effect, 20f);

            if (damageable)
            {
                var bloodPrefab = _bloodTrailPrefabs[Random.Range(0, _bloodTrailPrefabs.Length)];
                var trail = Instantiate(bloodPrefab, new Vector3(hit.point.x, 0, hit.point.z - 0.5f), Quaternion.identity);
                Destroy(trail, 20f);
            }
        }
        catch { }
    }

    private void SetTrailActive(TrailRenderer trail, bool active) => trail.gameObject.SetActive(active);

    public void Next(Vector3 startPoint, RaycastHit hit)
    {
        StartCoroutine(StartTrail(_trails[_index], startPoint, hit));

        _index++;
        if (_index >= _trails.Length)
            _index = 0;
    }
}