using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] private WeaponSO weaponSO;
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.EquipWeaponNetwork(weaponSO.ID);
    }
}
