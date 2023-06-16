using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public readonly List<GameObject> allFlockMember = new();
    public List<string> tagsToAvoid = new();

    [Range(1.0f, 10.0f)]
    public float neighbourDistance  = 4.0f;

    [Range(0.0f, 10.0f)]
    public float minDistance = 3.0f;

    [Range(0.0f, 10.0f)]
    public float updateInterval  = 1.0f;

    [Range(0.0f, 20.0f)]
    public float hunterSpottingDistance = 5.0f;

    [Range(0.0f, 50.0f)]
    public float hunterAvoidanceAmplifier = 10.0f;


    public void registerMember(GameObject member)
    {
        allFlockMember.Add(member);
    }

    
}
