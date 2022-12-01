using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private PickableItemSO _item;
    public PickableItemSO Item
    {
        get => _item;
        private set
        {
            _item = value;
            Instantiate(_item.Model, transform);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnPick()
    {
        Destroy();
    }

    public void Initialize(PickableItemSO item)
    {
        Item = item;
    }
}