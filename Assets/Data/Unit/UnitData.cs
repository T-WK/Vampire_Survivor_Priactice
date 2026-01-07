using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public int baseAttack;                      // 기본 공격력
    public float baseAttackSpeed;               // 기본 공격 속도
    public float projectileSpeed;               // 투사체 속도
    public float baseAttackRange;               // 기본 공격 범위
    public int baseMaxHp;                       // 기본 최대 체력
    public float baseMaxStamina;                // 기본 최대 스태미너
    public float baseMoveSpeed;                 // 기본 이동 속도
    public float baseCriticalChance;            // 기본 치명타 확률
    public float baseCriticalDamagePercent;     // 기본 치명타 피해량 배율
}
