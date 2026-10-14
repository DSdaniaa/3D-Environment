using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 5f, -7f);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSpeed = 0.05f;

    private float cameraYaw = 0f;

    private void Start()
    {
        if (target != null)
        {
            cameraYaw = target.eulerAngles.y;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Camera position behind the player
        Quaternion rotation = Quaternion.Euler(0f, cameraYaw, 0f);

        Vector3 targetPosition =
            target.position + rotation * offset;

        // Smoothly follow the player
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        // Camera looks in the same horizontal direction
        Quaternion targetRotation =
            Quaternion.Euler(0f, cameraYaw, 0f);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            followSpeed * Time.deltaTime
        );
    }

    public void RotateCamera(Vector2 delta)
    {
        cameraYaw += delta.x * rotationSpeed;
    }
}