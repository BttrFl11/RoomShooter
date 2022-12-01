using System;
using UnityEngine;

public class WeaponAnchor : MonoBehaviour
{
    private Weapon _weapon;
    public Weapon Weapon
    {
        get => _weapon;
        private set
        {
            _weapon = value;
            OnWeaponChanged?.Invoke(_weapon);
        }
    }

    public Action<Weapon> OnWeaponChanged;

    private void Start()
    {
        Weapon = GetComponentInChildren<Weapon>();
    }

    public void CreateNewWeapon(Weapon weaponPrefab)
    {
        var weapon = Instantiate(weaponPrefab, transform);

        Destroy(Weapon.gameObject);

        Weapon = weapon;
    }
}