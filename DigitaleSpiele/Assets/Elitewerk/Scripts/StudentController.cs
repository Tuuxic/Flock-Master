using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public float destroyYPosition = -10f;

    private void Start() {
        ScoreManager.instance.RegisterStudent();
    }

    private void Update()
    {
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
            ScoreManager.instance.UnregisterStudent();
        }
    }
}
