public enum Faction
{
    Player,
    Enemy,
    Neutral,
    Unknown
}

public interface IFactionMember
{
    Faction UnitFaction { get; set; }
    bool IsEnemy {get; }
    bool IsPlayer {get; }
}
