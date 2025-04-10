using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_ReturnState : E_BaseState
{
    private SkinLessZombie snail;
    private Vector3 origin;

    public Snailman_ReturnState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        snail = enemy as Snailman;
        origin = snail.OriginalPosition;
    }

    public override void Enter()
    {
        snail.Agent.speed = snail.patrolSpeed;
        snail.MoveTo(origin);
    }

    public override void Update()
    {
        if (Vector3.Distance(snail.transform.position, origin) < 0.3f)
        {
            fsm.ChangeState(new Snailman_PatrolState(snail, fsm, snail.PatrolPoints));
        }
    }
}
