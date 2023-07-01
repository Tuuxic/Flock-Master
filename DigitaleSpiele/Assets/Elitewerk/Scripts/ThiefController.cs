using UnityEngine;

public class ThiefController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (horizontalInput * Camera.main.transform.right + verticalInput * cameraForward).normalized;
        rb.velocity = movement * moveSpeed;

        // Rotate towards the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, toRotation, Time.deltaTime * 10f);
            animator.SetTrigger("WalkingStarted");
        }
        else
        {
            animator.SetTrigger("WalkingStopped");
        }
    }
}