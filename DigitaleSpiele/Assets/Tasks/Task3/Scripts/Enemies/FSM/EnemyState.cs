using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : IFSMState<EnemyController>
{
    protected const int PLAYER_RAYCAST_Y_OFFSET = 1;
    protected const int ENEMY_RAYCAST_Y_OFFSET = 2;
    // Player Out-of-sight chasing time
    protected const float PLAYER_OOS_CHASING_TIME = 1.5f;
    protected const float PLAYER_OOS_SEEKING_TIME = 3.0f;
    protected const float DEST_UPDATE_INTERVAL = 0.5f;

    public abstract void Enter(EnemyController entity);
    public abstract void Exit(EnemyController entity);
    public abstract void Reason(EnemyController entity);
    public abstract void Update(EnemyController entity);
}
