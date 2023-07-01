using UnityEngine;

public class ThiefCameraController : MonoBehaviour
{
    public Transform target;
    public float sensitivity = 5f;
    public float rotationYLimit = 45f;

    private Vector3 offset;
    private float rotationX, rotationY;

    void Start()
    {
        offset = transform.position - target.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Mouse X") * sensitivity;
        float verticalInput = Input.GetAxis("Mouse Y") * sensitivity;

        // Camera rotation
        rotationX += horizontalInput;
        rotationY -= verticalInput;
        rotationY = Mathf.Clamp(rotationY, -rotationYLimit, rotationYLimit);
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0f);
    
        // Camera position
        Vector3 desiredPosition = target.position + (rotation * offset);
        transform.position = desiredPosition;

        // Camera orientation
        transform.LookAt(target);
    }
}
