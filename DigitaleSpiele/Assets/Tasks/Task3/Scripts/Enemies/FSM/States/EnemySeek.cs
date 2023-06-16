using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySeek : EnemyState
{
   
    private float notSeePlayerTime = 0;

    private NavMeshAgent agent;

    public override void Enter(EnemyController e)
    {
        agent = e.GetComponent<NavMeshAgent>();
        agent.ResetPath();

    }

    public override void Exit(EnemyController e)
    {
        
    }

    public override void Reason(EnemyController e)
    {
        var canSeePlayer = false;

        // Can we see the player?
        RaycastHit hit;

        // Get enemy position and offset it for the raycast
        Vector3 enemyPos = e.transform.position;
        enemyPos.y = enemyPos.y + ENEMY_RAYCAST_Y_OFFSET;

        // Get player position and offset it for the raycast
        Vector3 playerPos = e.playerTransform.position;
        playerPos.y = playerPos.y + PLAYER_RAYCAST_Y_OFFSET;


        if (Physics.Raycast(enemyPos, playerPos - enemyPos, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                canSeePlayer = true;
                Debug.DrawRay(enemyPos, playerPos - enemyPos, Color.red);
            }
            else
            {
                Debug.DrawRay(enemyPos, playerPos - enemyPos, Color.green);
            }
        }

        if (!canSeePlayer)
        {
            notSeePlayerTime += Time.deltaTime;
        }
        else
        {
            e.ChangeState(new EnemyChase());
        }

        if (notSeePlayerTime > PLAYER_OOS_SEEKING_TIME) e.ChangeState(new EnemyPatrol());
    }

    public override void Update(EnemyController e)
    {
        // Wait until enemy see's player
    }
}
