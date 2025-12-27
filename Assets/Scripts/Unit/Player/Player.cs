using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Player : Unit, IHealable
{
    public float AttackSpeed => CurrentStat.AttackSpeed;
    private float _currentStamina;
    public float CurrentStamina
    {
        get { return _currentStamina; }
        set { _currentStamina = Mathf.Clamp(value, 0, CurrentStat != null ? CurrentStat.MaxStamina : 50f); }
    }
    public float ProjectileMoveSpeed => CurrentStat.ProjectileSpeed;

    void Awake()
    {
        Debug.Log("Player Awake");
        TargetProvider.Player = transform;
        Initialize(baseUnitData);
    }

    public override void Initialize(UnitData data)
    {
        // IfactionMember implementation
        UnitFaction = Faction.Player;

        CurrentStamina = data.baseMaxStamina;

        // Base initialization
        base.Initialize(data);
    }

    // IHealable implementation
    public void Heal(float amount)
    {
        currentHp += amount;
        if (currentHp > CurrentStat.MaxHp)
        {
            currentHp = CurrentStat.MaxHp;
        }
    }
}
