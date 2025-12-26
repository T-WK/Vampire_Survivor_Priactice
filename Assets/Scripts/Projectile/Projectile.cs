using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime = 5f;

    public void Init(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
