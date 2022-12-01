using UnityEngine;

[CreateAssetMenu(menuName = "SO/PickableItem")]
public class PickableItemSO : ScriptableObject
{
    [SerializeField] private PickableType _type;
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _prefab;

    public PickableType Type => _type;
    public GameObject Model => _model;
    public GameObject Prefab => _prefab;
}