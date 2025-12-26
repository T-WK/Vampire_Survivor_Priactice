using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private Unit Player;

    private void Awake() {
        Player = GetComponent<Unit>();
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Fire();
        }
    }

    void Fire()
    {
        float moveSpeed = Player.MoveSpeed;
        float attackDamage = Player.AttackDamage;
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Init(moveSpeed, attackDamage);
    }
}
