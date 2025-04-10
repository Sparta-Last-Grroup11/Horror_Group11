using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_ChaseState : E_BaseState
{
    private SkinLessZombie snail;

    public Snail_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        snail = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        snail.Agent.speed = snail.chaseSpeed;
    }

    public override void Update()
    {
        snail.MoveTo(snail.Player.position);

        // 플레이어가 멀어지면 Idle로
        if (!snail.IsPlayerFar())
        {
            fsm.ChangeState(new Snail_ReturnState(snail, fsm));
        }
    }
}
