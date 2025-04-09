using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_ChaseState : E_BaseState
{
    private SnailMan snail;

    public Snail_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        snail = enemy as SnailMan;
    }

    public override void Enter()
    {
        snail.StartAction();
    }

    public override void Update()
    {
        // 플레이어 위치로 이동 로직

        // 플레이어가 멀어지면 Idle로
        if (snail.IsPlayerFar())
        {
            fsm.ChangeState(new Snail_IdleState(snail, fsm));
        }
    }
}
