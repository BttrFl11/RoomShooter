using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerFighting))]
public class PlayerPicker : MonoBehaviour
{
    private PlayerFighting _playerFighting;

    private void Awake()
    {
        _playerFighting = GetComponent<PlayerFighting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PickableItem pickable))
        {
            switch (pickable.Item.Type)
            {
                case PickableType.Weapon:
                    PickupWeapon(pickable);
                    break;
            }

            pickable.OnPick();
        }
    }

    private void PickupWeapon(PickableItem pickable)
    {
        var prefab = pickable.Item.Prefab;
        var weaponPrefab = prefab.GetComponent<Weapon>();
        if (weaponPrefab == null)
            throw new System.Exception("Pickable item tagged \"Weapon\" doesn't contain Weapon component");

        _playerFighting.ChangeWeapon(weaponPrefab);
    }
}