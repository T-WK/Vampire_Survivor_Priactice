using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private Player Player;
    private float lastAttackTime = 0f;

    private void Awake() {
        Player = GetComponent<Player>();
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && lastAttackTime == 0f)
        {
            lastAttackTime = Time.time;
            Fire();
        }
    }

    void Update()
    {
        if (lastAttackTime > 0f)
        {
            float attackInterval = 1f / Player.AttackSpeed;
            if (Time.time - lastAttackTime >= attackInterval)
            {
                lastAttackTime = 0f;
            }
        }
    }

    void Fire()
    {
        float moveSpeed = Player.ProjectileMoveSpeed;
        float attackDamage = Player.AttackDamage;
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Init(moveSpeed, attackDamage);
    }
}
