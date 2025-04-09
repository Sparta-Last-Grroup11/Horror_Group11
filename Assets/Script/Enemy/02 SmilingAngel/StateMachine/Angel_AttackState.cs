using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_AttackState : E_BaseState  // 플레이어를 공격하는 상태
{
    private SmilingAngel angel;

    public Angel_AttackState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
    }

    public override void Enter()
    {
        angel.animator.SetTrigger("Attack");  // 공격 애니메이션 발동    
    }

    public override void Update()
    {
        if (angel.FinishAttack())  // 공격 애니메이션이 끝났을 때
        {
            // ex) GameManager.Instance.GameOver();  // 즉사하는 메서드 넣으면 될 듯? 
        }
    }
}
