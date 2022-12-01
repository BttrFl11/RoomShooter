using UnityEngine;

public class AIM : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private Camera _camera;

    [HideInInspector] public bool CanAIM = true;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (CanAIM)
            LookAtMouse();
    }

    private void LookAtMouse()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            var distance = hit.point - transform.position;
            var rotation = Quaternion.LookRotation(distance);
            transform.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
        }
    }
}