using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtMouse : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionReference lookPointAction; // <Mouse>/position

    [Header("Raycast")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask groundMask; // 바닥 레이어만
    [SerializeField] private float rayDistance = 500f;

    private InputAction _lookPoint;

    private void OnEnable()
    {
        if (cam == null) cam = Camera.main;

        _lookPoint = lookPointAction.action;
        _lookPoint.Enable();
    }

    private void OnDisable()
    {
        _lookPoint.Disable();
    }

    private void Update()
    {
        Vector2 screenPos = _lookPoint.ReadValue<Vector2>();

        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, groundMask))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y; // Y축 회전만

            Vector3 dir = target - transform.position;
            if (dir.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
        }
    }
}
