using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime = 5f;
    public Vector3 direction;

    public void Init(float speed, float damage, Vector3 direction)
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent<Unit>(out Unit unit);
        if (unit != null)
        {
            if (unit.UnitFaction == Faction.Enemy)
            {
                unit.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
