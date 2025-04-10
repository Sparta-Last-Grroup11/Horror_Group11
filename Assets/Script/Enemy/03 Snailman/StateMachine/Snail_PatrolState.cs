using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static SnailManFSM;

public class Snail_PatrolState : E_BaseState
{
    private SnailMan snail;
    private Transform[] wayPoints;
    private int currentIndex = 0;

    public Snail_PatrolState(Enemy enemy, E_StateMachine fsm, Transform[] patrolPoints) : base(enemy, fsm)
    {
        snail = enemy as SnailMan;
        wayPoints = patrolPoints;
    }

    public override void Enter()
    {
        // TODO: Idle 애니메이션 트리거
    }

    public override void Update()
    {
        if (snail.IsPlayerNear())
        {
            fsm.ChangeState(new Snail_ChaseState(snail, fsm));  // 추적Chase상태로 전환
        }
    }
}
