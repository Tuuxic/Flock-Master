using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

public class EnemyAttack : EnemyState
{

    private float timeInRange = 0;
    public override void Enter(EnemyController entity)
    {
        // Nothing
        Debug.Log("Entered Attack");
    }

    public override void Exit(EnemyController entity)
    {
        // Nothing
        Debug.Log("Exited Attack");
    }

    public override void Reason(EnemyController entity)
    {
        // Get enemy position
        Vector3 enemyPos = entity.transform.position;

        // Get player position
        Vector3 playerPos = entity.playerTransform.position;
        Vector3 distanceVec = playerPos - enemyPos; 

        if(distanceVec.magnitude > EnemyController.ATTACK_RANGE)
        {
            entity.ChangeState(new EnemyChase());
        }
        
    }

    public override void Update(EnemyController entity)
    {
        timeInRange += Time.deltaTime;

        if (timeInRange >= EnemyController.ATTACK_TIME)
        {
            // Damage Player
            GameEventManager.Raise(new PlayerDamageEvent(EnemyController.ATTACK_DAMAGE));

            // Reset Attack Time
            timeInRange = 0;

        }
    }

}
