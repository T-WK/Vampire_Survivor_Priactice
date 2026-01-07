using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 무기 모듈의 추가 스탯을 관리하는 클래스
/// </summary>

public enum AttackDirection
{
    Forward, Left, Right, Backward,
    ForwardLeft, ForwardRight, BackwardLeft, BackwardRight,
    AutoTarget
}

public class ModuleStat
{
    public string WeaponName { get; set; }
    public int TargetCount { get; set; }
    public float AttackDamage { get; set; }
    public float AttackInterval { get; set; }
    public float ProjectileMoveSpeed { get; set; }
    public float AttackRange { get; set; }
    public int ModuleLevel { get; set; }
    public int ProjectileCount { get; set; }
    public List<AttackDirection> AttackDirections = new List<AttackDirection>();

    public ModuleStat(WeaponData weaponData)
    {
        WeaponName = weaponData.weaponName;
        TargetCount = weaponData.targetCount;
        AttackDamage = weaponData.attackDamage;
        AttackInterval = weaponData.attackInterval;
        ProjectileMoveSpeed = weaponData.projectileMoveSpeed;
        AttackRange = weaponData.attackRange;

        AttackDirections.Add(AttackDirection.Forward);

        ModuleLevel = 1;
        ProjectileCount = 1;
    }

    public void UpgradeModule(WeaponData weaponData)
    {
        if (ModuleLevel >= 3)
        {
            return; // 최대 레벨 도달 시 업그레이드 불가
        }

        ModuleLevel += 1;

        AttackDamage += weaponData.upgradeAttackDamage;
        AttackInterval += weaponData.upgradeAttackInterval;
        ProjectileMoveSpeed += weaponData.upgradeProjectileMoveSpeed;
        AttackRange += weaponData.upgradeAttackRange;
    }

    public void AddAttackDirection(AttackDirection direction)
    {
        if (!AttackDirections.Contains(direction))
        {
            AttackDirections.Add(direction);
            ProjectileCount = AttackDirections.Count;
        }
    }
}
