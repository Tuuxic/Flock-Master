using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class ThiefController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;

    public TwoBoneIKConstraint twoBoneIKConstraint;
    public Transform leftHandTarget;

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

        // Dab
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Dab");
        }


        //twoBoneIKConstraint.weight = Mathf.Lerp(0, 1, t);
        //t += 0.1f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] cans = GameObject.FindGameObjectsWithTag("Can");
            GameObject closestCan = null;

            float minDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject can in cans)
            {
                float distance = Vector3.Distance(can.transform.position, currentPosition);
                if (distance < minDistance)
                {
                    closestCan = can;
                    minDistance = distance;
                }
            }

            if (closestCan != null)
            {
                leftHandTarget.position = closestCan.transform.position;
                twoBoneIKConstraint.weight = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
            twoBoneIKConstraint.weight = 0;
        }
    }

    //private float t = 0f;
}