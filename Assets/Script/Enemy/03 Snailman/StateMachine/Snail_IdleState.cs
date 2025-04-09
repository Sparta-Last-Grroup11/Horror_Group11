using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SnailManFSM;

public class Snail_IdleState : E_BaseState
{
    private SnailMan snail;

    public Snail_IdleState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        snail = enemy as SnailMan;
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
