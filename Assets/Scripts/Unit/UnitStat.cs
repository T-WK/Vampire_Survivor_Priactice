using UnityEngine;

[CreateAssetMenu(fileName = "UnitStat", menuName = "Scriptable Objects/UnitStat")]
public class UnitStat
{
    public int Attack;                      // 공격력
    public float AttackSpeed;               // 공격 속도
    public float ProjectileSpeed;           // 투사체 속도
    public float AttackRange;               // 공격 범위
    public int MaxHp;                       // 최대 체력
    public float MaxStamina;                // 최대 스태미너
    public float MoveSpeed;                 // 이동 속도
    public float CriticalChance;            // 치명타 확률
    public float CriticalDamagePercent;     // 치명타 피해량 배율

    public UnitStat(UnitData baseData)
    {
        Attack = baseData.baseAttack;
        AttackSpeed = baseData.baseAttackSpeed;
        ProjectileSpeed = baseData.projectileSpeed;
        AttackRange = baseData.baseAttackRange;
        MaxHp = baseData.baseMaxHp;
        MaxStamina = baseData.baseMaxStamina;
        MoveSpeed = baseData.baseMoveSpeed;
        CriticalChance = baseData.baseCriticalChance;
        CriticalDamagePercent = baseData.baseCriticalDamagePercent;
    }

    public void ApplyModifier(StatModifier modifier)
    {
        Attack += modifier.addAttack;           // 공격력
        AttackSpeed += modifier.addAttackSpeed; // 공속
        AttackRange += modifier.addAttackRange; // 사거리
        ProjectileSpeed += modifier.addProjectileSpeed; // 투사체 속도
        MaxHp += modifier.addMaxHp;             // 최대 체력
        MaxStamina += modifier.addMaxStamina;   // 최대 스태미너
        MoveSpeed += modifier.addMoveSpeed;    // 이동 속도
        CriticalChance += modifier.addCriticalChance;   // 치명타 확률
        CriticalDamagePercent += modifier.addCriticalDamagePercent; // 치명타 피해량 배율
    }
}