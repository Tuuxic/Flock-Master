using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : StatefulMonoBehaviour<EnemyController> {

    [SerializeField] public Transform playerTransform;
    [SerializeField] public GameObject waypointRoot;
	[SerializeField] public List<Vector3> waypoints = new();

    public const float ATTACK_RANGE = 3f;
    public const float ATTACK_TIME = 0.75f;
    public const int ATTACK_DAMAGE = 200;


    void Awake() {
		Transform[] wayPoints = new Transform[0];

        if (waypointRoot != null)
		{
            wayPoints = waypointRoot.GetComponentsInChildren<Transform>();
        }
		
		// filter out the root objects position
		foreach( Transform t in wayPoints ) {
			if( !t.Equals( waypointRoot.transform ) )
				waypoints.Add( t.position );
		}

		fsm = new FSM<EnemyController>();
		fsm.Configure(this, new EnemyPatrol());
    }
}
