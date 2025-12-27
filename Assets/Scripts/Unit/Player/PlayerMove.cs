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
        float multiplier = 1f;


        if (Keyboard.current.leftShiftKey.isPressed)
        {
            multiplier = 1.5f; // 달리기 속도 증가
            player.CurrentStamina -= 10f * Time.deltaTime; // 스태미나 감소
        }
        else
        {
            player.CurrentStamina += 5f * Time.deltaTime; // 스태미나 회복
        }

        Debug.Log("Current Stamina: " + player.CurrentStamina);

        if (player.CurrentStamina <= 0f)
        {
            multiplier = 0.5f; // 스태미나가 없으면 속도 감소
        }

        transform.position += move * speed * Time.deltaTime * multiplier;

        if (anim != null)
        {
            anim.SetFloat("Speed", move.magnitude);
        }
    }
}
