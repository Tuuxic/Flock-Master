using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterScript : MonoBehaviour
{

    public FlockManager targetFlock;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 hunterPos = gameObject.transform.position;
        List<GameObject> flockList = targetFlock.allFlockMember;
        float shortestDist = float.PositiveInfinity;
        GameObject shortestDistMember = gameObject;

        foreach (GameObject flockMember in flockList) { 
            Vector3 posFlockMember = flockMember.transform.position;
            Vector3 distVec = posFlockMember - hunterPos;
            if(distVec.sqrMagnitude < shortestDist)
            {
                shortestDistMember = flockMember;
                shortestDist = distVec.sqrMagnitude;
            }
        
        }

        agent.SetDestination(shortestDistMember.transform.position);

    }
}
