using System.Collections.Generic;
using UnityEngine;

public class Player : Unit, IHealable
{
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
