using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Player player;
    private Vector2 movement;
    private Animator anim;

    void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void Update()
    {
        float h = movement.x; // A, D
        float v = movement.y;   // W, S
        float speed = player.MoveSpeed;

        Vector3 move = new Vector3(h, 0f, v).normalized;

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            move *= 1.5f; // 달리기 속도 증가
        }

        transform.position += move * speed * Time.deltaTime;

        if (anim != null)
        {
            anim.SetFloat("Speed", move.magnitude);
        }
    }
}
