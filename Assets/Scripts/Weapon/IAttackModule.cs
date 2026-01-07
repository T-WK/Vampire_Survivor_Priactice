using UnityEngine;

public enum AttackType
{
    None,           // 기본값 (오류 방지)
    Projectile,     // 투사체 공격 (화살, 총알 등)
    Ranged,         // 범위 공격 (화염방사기, 레이저, 베기 등)
    Magic,          // 마법 공격 (스펠, 마나 소비)
    AreaOfEffect,   // 범위 공격 (AOE)
}

public interface IAttackModule
{
    void Init(WeaponData weaponData, Transform firePoint);
    void Tick(float deltaTime);
    void Fire(IDamageable[] targets, int targetCount, UnitStat unitStat);
    public int TargetCount { get; }
}