using UnityEngine;

public class Enemy : Unit
{
    void Awake()
    {
        Initialize(baseUnitData);
    }

    public override void Initialize(UnitData data)
    {
        // IfactionMember implementation
        UnitFaction = Faction.Enemy;

        // Base initialization
        base.Initialize(data);
    }

    void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Unit>(out Unit unit);
        if (unit != null)
        {
            if (UnitFaction == Faction.Enemy && unit.UnitFaction == Faction.Player)
            {
                unit.TakeDamage(CurrentStat.Attack);
            }
        }
    }
}
