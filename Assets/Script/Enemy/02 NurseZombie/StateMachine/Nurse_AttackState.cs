using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse_AttackState : E_BaseState  // 플레이어를 공격하는 상태
{
    private Nurse nurse;

    public Nurse_AttackState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurse = enemy as Nurse;
    }

    public override void Enter()
    {
        nurse.nurseAnimator.SetTrigger("IsAttacking");  // 공격 애니메이션 발동    
    }

    public override void Update()
    {
        if (nurse.FinishAttack())  // 공격 애니메이션이 끝났을 때
        {
            // ex) GameManager.Instance.GameOver();  // 즉사하는 메서드 넣으면 될 듯? 
        }
    }
}
