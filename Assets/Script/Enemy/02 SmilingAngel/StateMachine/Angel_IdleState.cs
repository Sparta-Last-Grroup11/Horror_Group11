using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SmilingAngelFSM;

public class Angel_IdleState : E_State  // 기본 상태일 때 
{
    private SmilingAngel angel;

    public Angel_IdleState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
    }

    public override void Enter()
    {
        // TODO: Idle 애니메이션 트리거
    }

    public override void Update()
    {
        if (angel.IsLightOn())
            return;

        if (angel.CanSeePlayerFace() && angel.IsPlayerLookingAtMe())   // 1) 웃는천사가 플레이어를 볼 수 있는 상태(어둠), 2) 웃는천사가 플레이어와 마주쳤을 때  
        {
            fsm.ChangeState(new Angel_ChaseState(angel, fsm));  // 2) 추적Chase 상태로 전환
        }
    }
}
