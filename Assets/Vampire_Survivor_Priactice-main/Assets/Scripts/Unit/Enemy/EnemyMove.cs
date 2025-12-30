using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Enemy enemy;
    private float rotateSpeed = 10f;
    private Rigidbody rd;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        rd = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 dir = TargetProvider.Player.position - transform.position;
        dir.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        rd.MoveRotation(
            Quaternion.Slerp(
                rd.rotation,
                targetRot,
                rotateSpeed * Time.fixedDeltaTime
            )
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            TargetProvider.Player.position,
            enemy.MoveSpeed * Time.deltaTime
        );
    }
}
