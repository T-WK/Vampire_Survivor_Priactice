using NUnit.Framework.Constraints;
using UnityEngine;

public class AddWeapone : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public void AddProjectileWeaponToPlayer()
    {
        var WeaponController = TargetProvider.WeaponController;
        var projectileModule = new ProjectileAttackModule();
        projectileModule.Init(weaponData, firePoint);

        WeaponController.AddAttackModule(projectileModule);
    }
}
