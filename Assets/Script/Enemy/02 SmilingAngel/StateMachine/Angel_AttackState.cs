using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_AttackState : E_State  // 플레이어를 공격하는 상태
{
    private SmilingAngel angel;

    public Angel_AttackState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
    }

    public override void Enter()
    {
        angel.Attack();
    }

    public override void Update()
    {
        if (angel.HasFinishedAttack())
        {
            fsm.ChangeState(new Angel_IdleState(angel, fsm));
        }
    }
}
