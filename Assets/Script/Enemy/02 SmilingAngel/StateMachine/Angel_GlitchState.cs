using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_GlitchState : E_BaseState   // 적을 바라볼 때 글리치가 일어나는 상태
{
    private SmilingAngel angel;
    private Angel_ChaseState returnState;  // 돌아갈 상태 보관

    public Angel_GlitchState(Enemy enemy, E_StateMachine fsm, Angel_ChaseState chaseState) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
        returnState = chaseState;
    }

    public override void Enter()
    {
        // ex) UiEffect.instance.PlayGlitch();  // 글리치 효과 UI(이펙트, 소리 등) 이 위치에 들어감 
    }

    public override void Update()
    {
        if (!angel.IsPlayerLookingAtMe())  // 플레이어가 나를 보지 않을 때
        {
            // 글리치 효과 꺼지고
            fsm.ChangeState(returnState);  // 원래 상태로 돌아감 (즉, 재추격 시작)
        }

    }

    public override void Exit()
    {
        // 글리치 효과를 완전히 꺼주는 것도 여기에 들어가야 할 듯. 
        fsm.ChangeState(new Angel_IdleState(angel, fsm));  // 글리치 상태 풀리고 Idle 상태로 전환.
    }
}
