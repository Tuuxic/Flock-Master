using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

public class DestinationBehaviour : MonoBehaviour
{
    
    public FlockManager targetFlock;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (targetFlock == null) return;
        count = targetFlock.allFlockMember.Count;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Flock") return;

        count--;
        Destroy(other.gameObject);

        if (count <= 0) GameEventManager.Raise(new GameWinEvent());
    }

}
