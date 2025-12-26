using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour, IDamageable, IFactionMember
{
    public UnitData baseUnitData;           // 플레이어의 기본 유닛 데이터
    public UnitStat CurrentStat; // 현재 플레이어의 스탯
    public List<StatModifier> modifiers;

    public float currentHp;
    public float currentStamina;

    public float MoveSpeed => CurrentStat.MoveSpeed;
    public float CurrentStamina => currentStamina;
    public float AttackDamage => CurrentStat.Attack;

    public virtual void Initialize(UnitData data)
    {
        currentHp = data.baseMaxHp;
        currentStamina = data.baseMaxStamina;

        baseUnitData = data;
        modifiers = new List<StatModifier>();
        CurrentStat = new UnitStat(data);

        RecalculateStat();
    }

    public virtual void RecalculateStat()
    {
        foreach (var mod in modifiers)
        {
            CurrentStat.ApplyModifier(mod);
        }
    }

    // IFactionMember implementation
    public Faction UnitFaction { get; set; } = Faction.Unknown;
    public bool IsEnemy => UnitFaction == Faction.Enemy;
    public bool IsPlayer => UnitFaction == Faction.Player;

    // IDamageable implementation
    public float InvincibleLength => 2.0f; // 무적 시간 (초)
    public float InvincibleCounter { get; set; } = 0f;
    public bool IsInvincible => InvincibleCounter > 0f;
    public void TakeDamage(float damage)
    {
        if (IsInvincible)
        {
            Debug.Log(UnitFaction + " is invincible and took no damage.");
            return;
        }

        currentHp -= damage;
        InvincibleCounter = IsPlayer ? InvincibleLength : 0f;

        Debug.Log(UnitFaction + " took " + damage + " damage. Current HP: " + currentHp);

        if (IsDead)
        {
            Debug.Log(UnitFaction + " is dead.");

            switch (UnitFaction)
            {
                case Faction.Player:
                    // 플레이어 사망 처리 로직 추가
                    Debug.Log("Player has died. Implement respawn or game over logic here.");
                    break;
                case Faction.Enemy:
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public bool IsDead => currentHp <= 0;

    void Update()
    {
        if (InvincibleCounter > 0f)
        {
            InvincibleCounter -= Time.deltaTime;
        }
    }
}
