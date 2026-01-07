using System.Reflection;
using UnityEngine;

public class ProjectileAttackModule : IAttackModule
{
    private WeaponData weaponData;
    public ModuleStat moduleStat;
    private Transform firePoint;
    private float cooldownTimer;

    public int TargetCount => weaponData != null ? weaponData.targetCount : 0;

    public WeaponID WeaponID => weaponData != null ? weaponData.weaponID : WeaponID.None;

    public void Init(WeaponData weaponData, Transform firePoint)
    {
        this.weaponData = weaponData;
        this.firePoint = firePoint;
        cooldownTimer = 0f;

        moduleStat = new ModuleStat(weaponData);
    }

    public void Tick(float deltaTime)
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= deltaTime;
        }
    }

    public void Fire(IDamageable[] targets, int targetCount, UnitStat unitStat)
    {
        if (cooldownTimer > 0f)
        {
            return; // 쿨다운 중이면 발사하지 않음
        }

        Debug.Log(moduleStat.ToString());

        float damage = moduleStat.AttackDamage + unitStat.Attack;
        float speed = moduleStat.ProjectileMoveSpeed + unitStat.ProjectileSpeed;

        foreach (var direction in moduleStat.AttackDirections)
        {
            // 투사체 생성 및 초기화
            GameObject projectileObject = GameObject.Instantiate(weaponData.projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            Vector3 dir;

            switch (direction)
            {
                case AttackDirection.Forward:
                    dir = Vector3.forward;
                    break;
                case AttackDirection.Left:
                    dir = Vector3.left;
                    break;
                case AttackDirection.Right:
                    dir = Vector3.right;
                    break;
                case AttackDirection.Backward:
                    dir = Vector3.back;
                    break;
                case AttackDirection.ForwardLeft:
                    dir = (Vector3.forward + Vector3.left).normalized;
                    break;
                case AttackDirection.ForwardRight:
                    dir = (Vector3.forward + Vector3.right).normalized;
                    break;
                case AttackDirection.BackwardLeft:
                    dir = (Vector3.back + Vector3.left).normalized;
                    break;
                case AttackDirection.BackwardRight:
                    dir = (Vector3.back + Vector3.right).normalized;
                    break;
                case AttackDirection.AutoTarget:
                default:
                    dir = Vector3.forward;
                    break;
            }

            projectile.Init(speed, damage, dir);
        }

        // 쿨다운 초기화
        cooldownTimer = 1f / weaponData.attackInterval;
    }

    public void ModuleLevelUP()
    {
        moduleStat.UpgradeModule(weaponData);
    }
}
