using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseZombie_AttackState : E_BaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    public NurseZombie_AttackState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseAnimator.SetTrigger("IsAttacking");  // 공격 애니메이션 발동    
    }

    public override void Update()
    {
        if (nurseZombie.FinishAttack())  // 공격 애니메이션이 끝났을 때
        {
            // ex) GameManager.Instance.GameOver();  // 즉사하는 메서드 넣으면 될 듯? 
        }
    }
}
