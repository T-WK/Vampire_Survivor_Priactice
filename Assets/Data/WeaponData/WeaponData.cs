using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponID weaponID;
    public string weaponName;
    public float attackDamage;
    public float attackInterval;
    public float projectileMoveSpeed;
    public float attackRange;
    public int targetCount;

    public Sprite weaponIcon;
    public GameObject projectilePrefab;
    public AttackType attackType;

    // Upgrade stats
    public float upgradeAttackDamage;
    public float upgradeAttackInterval;
    public float upgradeProjectileMoveSpeed;
    public float upgradeAttackRange;
}
