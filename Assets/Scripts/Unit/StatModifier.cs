using UnityEngine;

[CreateAssetMenu(fileName = "StatModifier", menuName = "Scriptable Objects/StatModifier")]
public class StatModifier : ScriptableObject
{
    public int addAttack;                      // 추가 공격력
    public float addAttackSpeed;               // 추가 공격 속도
    public float addProjectileSpeed;           // 추가 투사체 속도
    public float addAttackRange;               // 추가 공격 범위
    public int addMaxHp;                       // 추가 최대 체력
    public float addMaxStamina;                // 추가 최대 스태미너
    public float addMoveSpeed;                 // 추가 이동 속도
    public float addCriticalChance;            // 추가 치명타 확률
    public float addCriticalDamagePercent;     // 추가 치명타 피해량 배율
}
