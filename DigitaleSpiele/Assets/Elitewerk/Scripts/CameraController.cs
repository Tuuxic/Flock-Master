using UnityEngine;

public class Follow_player : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}