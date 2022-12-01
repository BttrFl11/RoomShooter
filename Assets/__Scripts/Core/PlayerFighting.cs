using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerFighting : MonoBehaviour
{
    [SerializeField] private Animator _shootAnimator;

    private WeaponAnchor _weaponAnchor;
    private Weapon _weapon;

    private bool CanShoot { get => _weapon.CanShoot; }
    private bool GotInput { get => InputManager.Instance.GetShootInput; }

    private void Awake()
    {
        _weaponAnchor = GetComponentInChildren<WeaponAnchor>();
    }

    private void OnEnable()
    {
        _weaponAnchor.OnWeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _weaponAnchor.OnWeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        _weapon = weapon;

        _shootAnimator.speed = _weapon.FireRate;
    }

    private void Update()
    {
        if (_weapon == null)
            return;

        if (CanShoot && GotInput)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _weapon.Attack();

        _shootAnimator.SetTrigger("Shoot");
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _weaponAnchor.CreateNewWeapon(weapon);
    }
}