using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilingAngel : Enemy   // 웃는 천사 기믹
{
    public Animator animator { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        fsm = new SmilingAngelFSM(this);
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - Player.position).normalized;  // 플레이어에서 몬스터를 향하는 방향 벡터
        Vector3 playerforward = Player.forward.normalized;  // 플레이어가 보고 있는 방향 벡터

        float dot = Vector3.Dot(toNurse, playerforward);  // 1에 가까울수록 플레이어 = 몬스터 같은 방향
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
    }

    public bool FinishAttack()  // 공격 애니메이션이 끝났는지 확인하는 메서드
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return !(stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f);
    }


    public bool IsLightOn()
    {
        /* TODO */
        return false;
    }

    public bool IsPlayerInRoom()
    {
        /* TODO */
        return true;
    }

    public bool IsNearPlayer()
    {
        /* TODO */
        return false;
    }

    public bool IsPlayerLookingAway()
    { /* TODO */ return false; }

    public bool ChaseTimeExceeded() // 추격하던 적이 일정 시간 되면 추격 종료
    { /* TODO */ return false; }





    public override void StartAction()
    {
        // 애니메이션, 사운드 넣어주면 됨
    }

    public override void Attack()
    {
        // 공격 애니메이션, 데미지 적용 등
    }
    
}
