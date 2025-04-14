using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseZombie_AttackState : E_BaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    private float attackRange = 0.5f; // 공격 범위
    private float dashSpeed = 8f; // 돌진 속도, 일반 추격보다 빠르게
    private bool hasDashed = false; // 돌진 완료 여부

    public NurseZombie_AttackState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        hasDashed = false;
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return; 

        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);

        if (!hasDashed)
        {
            if (distance > attackRange)
            {
                nurseZombie.MoveTowardsPlayer(dashSpeed);
            }
            else if (distance <= attackRange && distance > 0.2f)
            {
                nurseZombie.MoveTowardsPlayer(dashSpeed * 0.5f);
            }
            else
            {
                hasDashed = true;
                nurseZombie.nurseAnimator.SetTrigger("Attack"); 
            }
        }
        else
        {
            if (nurseZombie.FinishAttack())  // 공격 애니메이션이 끝났을 때
            {
                // ex) GameManager.Instance.GameOver();  // 즉사하는 ui 띄우는 메서드
            }
        }
 
    }
}
