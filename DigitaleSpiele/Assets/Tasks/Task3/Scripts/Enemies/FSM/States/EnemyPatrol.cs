using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyState {

	

    private int currentWaypoint = 0;
    private NavMeshAgent agent;


    public override void Enter(EnemyController e) {
        agent = e.GetComponent<NavMeshAgent>();

		if(e.waypoints.Count > 0)
		{
            agent.SetDestination(e.waypoints[currentWaypoint]);
        }
       
	}

	public override void Exit(EnemyController e) {
		// Exit Behaviour
	}
		
	public override void Reason(EnemyController e) {
        
		// Can we see the player? if so, we gotta chase after him!
		RaycastHit hit;

		// Get enemy position and offset it for the raycast
		Vector3 enemyPos = e.transform.position;
		enemyPos.y = enemyPos.y + ENEMY_RAYCAST_Y_OFFSET;

		// Get player position and offset it for the raycast
		Vector3 playerPos = e.playerTransform.position;
		playerPos.y = playerPos.y + PLAYER_RAYCAST_Y_OFFSET;

        if ( Physics.Raycast( enemyPos, playerPos - enemyPos, out hit ) ) {

			if (hit.transform.tag == "Player"){

				Debug.DrawRay(enemyPos,playerPos - enemyPos, Color.red);
				e.ChangeState(new EnemyChase());

			} else {
				Debug.DrawRay(enemyPos, playerPos - enemyPos, Color.green);
			}
		}
	}
		
	public override void Update( EnemyController e ) {
		if (e.waypoints.Count < 0) return;

		if (agent.remainingDistance <= agent.stoppingDistance) {

			currentWaypoint = (currentWaypoint + 1) % e.waypoints.Count;

			agent.SetDestination(e.waypoints[currentWaypoint]);
		}

	}
}
