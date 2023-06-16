using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.AI;

public class Flock : MonoBehaviour
{
    [SerializeField] private FlockManager manager;

    private NavMeshAgent agent;
    private ThirdPersonControllerAI controller;

    private float timeSinceUpdate = 0;

    void Awake()
    {
        manager.registerMember(gameObject);
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<ThirdPersonControllerAI>();
    }

    void Update()
    {
        ApplyRules();
    }


    private void ApplyRules()
    {
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate < manager.updateInterval) return;

        CalcFlockCenterAndFlockAvoid(out Vector3 flockCenter, out Vector3 flockAvoid);
        CalcHunterAvoid(out Vector3 hunterAvoid);

        flockCenter += (hunterAvoid + flockAvoid) / 2;

        agent.SetDestination(flockCenter);
        
    }

    private void CalcFlockCenterAndFlockAvoid(out Vector3 flockCenter, out Vector3 flockAvoid)
    {
        flockCenter = Vector3.zero;
        flockAvoid = Vector3.zero;
        int groupSize = 0;
        int avoidSize = 0;

        List<GameObject> flockMember = manager.allFlockMember;
        foreach (GameObject memeber in flockMember)
        {
            if (memeber == this.gameObject) continue;

            float memDist = Vector3.Distance(memeber.transform.position, this.transform.position);
            if (memDist <= manager.neighbourDistance)
            {
                flockCenter += memeber.transform.position;
                groupSize++;

                if (memDist < manager.minDistance)
                {
                    flockAvoid += (this.transform.position - memeber.transform.position);
                    avoidSize++;
                }


            }

        }

        flockCenter = groupSize != 0 ? (flockCenter / groupSize) : (this.transform.position);
        flockAvoid = avoidSize != 0 ? (flockAvoid / avoidSize) : (flockAvoid);
    }


    private void CalcHunterAvoid(out Vector3 hunterAvoid)
    {
        List<GameObject> avoidObjects = new();
        foreach (string tag in manager.tagsToAvoid)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag(tag);
            avoidObjects.AddRange(go);
        }

        hunterAvoid = Vector3.zero;
        int groupSize = 0;

        foreach (GameObject memeber in avoidObjects)
        {
            
            float memDist = Vector3.Distance(memeber.transform.position, this.transform.position);
            if (memDist > manager.hunterSpottingDistance) continue;

            Vector3 distVec = (memeber.transform.position - this.transform.position);
            hunterAvoid -= distVec.normalized * manager.hunterAvoidanceAmplifier;

        }


        controller.Sprinting = groupSize != 0;

        hunterAvoid = groupSize != 0 ? (hunterAvoid / groupSize) : (hunterAvoid);
    }


    void OnDestroy()
    {
        manager.removeMember(gameObject);
    }

}