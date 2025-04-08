using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_ChaseState : E_State    // 플레이어를 추격하는 상태일 때
{
    private SmilingAngel angel;
    private float chaseTimer;
    private const float maxChaseTime = 5f;

    public Angel_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
    }

    public override void Enter()
    {
        chaseTimer = 0f;
        angel.StartAction();   // 추격하는 행동 시작
    }

    public override void Update()
    {
        if (angel.IsLightOn())
            return;

        if (!angel.IsPlayerInRoom())  // 플레이어가 방 밖으로 나갈 때(즉 복도일 때)
        {
            if (angel.IsNearPlayer())  // 천사가 일정 거리 안에 있다면
            {
                fsm.ChangeState(new Angel_AttackState(angel, fsm));  // 공격 상태로 전환
                return;
            }
        }
        else // 방 안에서 대기중일 때는
        {
            chaseTimer += Time.deltaTime;

            if (chaseTimer >= maxChaseTime)  // chaseTimer가 일정 시간 이상 지나면 
            {
                fsm.ChangeState(new Angel_IdleState(angel, fsm));  // 추격 상태 풀리고 Idle 상태로 전환
                return;
            }
        }

        if (angel.IsPlayerLookingAtMe())  // 플레이어와 보고 있을 때 
        {
            fsm.ChangeState(new Angel_GlitchState(angel, fsm, this));  // 지직거리는 글리치 상태로 전환
        }
    }
}
