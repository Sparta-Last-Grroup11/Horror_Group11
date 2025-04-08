using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_GlitchState : E_State   // 적을 바라볼 때 글리치가 일어나는 상태
{
    private SmilingAngel angel;
    private E_State previousState;

    public Angel_GlitchState(Enemy enemy, E_StateMachine fsm, E_State prevState) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
        previousState = prevState;
    }

    public override void Enter()
    {
        // ex) UiEffect.instance.PlayGlitch();  // 글리치 효과 UI(이펙트, 소리 등) 이 위치에 들어감 
    }

    public override void Update()
    {
        if (angel.IsPlayerLookingAway())  // 플레이어가 쳐다보고 있는 동안에는
        {
            fsm.ChangeState(previousState);  // 글리치 효과 계속 유지
        }
    }

    public override void Exit()
    {
        fsm.ChangeState(new Angel_IdleState(angel, fsm));  // 글리치 상태 풀리고 Idle 상태로 전환.
    }
}
