using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : EnemyState {


    private float notSeePlayerTime = 0;
    private float chasingTime = 0;
    private NavMeshAgent agent;
	private ThirdPersonControllerAI controller;

    public override void Enter(EnemyController e) {
		agent = e.GetComponent<NavMeshAgent>();
        agent.SetDestination(e.playerTransform.position);
		controller = e.GetComponent<ThirdPersonControllerAI>();
		controller.Sprinting = true;

    }

	public override void Exit(EnemyController e) {
        controller.Sprinting = false;
    }

	public override void Reason(EnemyController e) {

        var canSeePlayer = false;

        // Can we see the player? If not, we get out of here
        RaycastHit hit;

        // Get enemy position and offset it for the raycast
        Vector3 enemyPos = e.transform.position;
        enemyPos.y = enemyPos.y + ENEMY_RAYCAST_Y_OFFSET;

        // Get player position and offset it for the raycast
        Vector3 playerPos = e.playerTransform.position;
        playerPos.y = playerPos.y + PLAYER_RAYCAST_Y_OFFSET;


		if( Physics.Raycast( enemyPos, playerPos - enemyPos, out hit ) ) {
            if( hit.transform.tag == "Player" ) {
                canSeePlayer = true;
				Debug.DrawRay(enemyPos, playerPos - enemyPos, Color.red);
			} else {
				Debug.DrawRay(enemyPos, playerPos - enemyPos, Color.green);
			}
		}

		if (!canSeePlayer) {
			notSeePlayerTime += Time.deltaTime;
		} else {
			notSeePlayerTime = 0;
		}

		if (notSeePlayerTime > PLAYER_OOS_CHASING_TIME) e.ChangeState(new EnemySeek());

        enemyPos = e.transform.position;
        playerPos = e.playerTransform.position;
        Vector3 distanceVec = playerPos - enemyPos;
        if (distanceVec.magnitude < EnemyController.ATTACK_RANGE)
        {
            e.ChangeState(new EnemyAttack());
        }
    }
		
	public override void Update( EnemyController e ) {

		chasingTime += Time.deltaTime;

		if (chasingTime > DEST_UPDATE_INTERVAL)
		{
			agent.SetDestination(e.playerTransform.position);
			chasingTime = 0;
		}

    }
}
