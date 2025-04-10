using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static SnailManFSM;

public class Snail_PatrolState : E_BaseState
{
    private SkinLessZombie snail;
    private Transform[] wayPoints;
    private int currentIndex = 0;

    public Snail_PatrolState(Enemy enemy, E_StateMachine fsm, Transform[] patrolPoints) : base(enemy, fsm)
    {
        snail = enemy as SkinLessZombie;
        wayPoints = patrolPoints;
    }

    public override void Enter()
    {
        snail.Agent.speed = snail.patrolSpeed;
        snail.MoveTo(wayPoints[currentIndex].position);
    }

    public override void Update()
    {
        if (snail.IsPlayerInRange())
        {
            fsm.ChangeState(new SnailMan_ChaseState(snail,fsm));
            return;
        }

        if (snail.Agent.remainingDistance < 0.2f)
        {
            currentIndex = (currentIndex + 1) % wayPoints.Length;
            snail.MoveTo(wayPoints[currentIndex].position);
        }

        if (snail.IsPlayerNear())
        {
            fsm.ChangeState(new Snail_ChaseState(snail, fsm));  // 추적Chase상태로 전환
        }
    }
}
