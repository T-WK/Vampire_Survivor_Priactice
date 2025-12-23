using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 20, -30);
    public float zoomSpeed = 1f;

    public void OnMouseScrollY(InputValue value)
    {
        float zoomDelta = value.Get<float>() * zoomSpeed;
        offset.y -= zoomDelta * 1f;
        offset.z += zoomDelta * 1.5f;

        offset.y = Mathf.Clamp(offset.y, 1f, 20f);
        offset.z = Mathf.Clamp(offset.z, -30f, -1.5f);
    }

    void Update()
    {
        transform.position = TargetProvider.Player.position + offset;
    }

    void LateUpdate()
    {
        transform.position = TargetProvider.Player.position + offset;
    }
}
