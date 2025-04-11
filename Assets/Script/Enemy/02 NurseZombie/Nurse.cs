using UnityEngine;

public class Nurse : Enemy   // 웃는 천사 기믹
{
    public Animator nurseAnimator { get; private set; }
    public float moveSpeed = 4.0f;  // 이동 속도
    public float attackRange = 2f;  // 공격 범위

    protected override void Awake()
    {
        base.Awake();
        nurseAnimator = GetComponentInChildren<Animator>();

        InitNurseFSM();
    }

    private void InitNurseFSM()
    {
        fsm = new E_StateMachine();
        fsm.ChangeState(new Nurse_IdleState(this, fsm));
        Debug.Log("FSM 작동 돼?");
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - playerTransform.position).normalized;  // 플레이어에서 몬스터를 향하는 방향 벡터
        Vector3 playerforward = playerTransform.forward.normalized;  // 플레이어가 보고 있는 방향 벡터

        float dot = Vector3.Dot(toNurse, playerforward);  // 1에 가까울수록 플레이어 = 몬스터 같은 방향
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
    }

    public bool FinishAttack()  // 공격 애니메이션이 끝났는지 확인하는 메서드
    {
        AnimatorStateInfo stateInfo = nurseAnimator.GetCurrentAnimatorStateInfo(0);
        return !(stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f);
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= attackRange;  // 공격 범위 안에 들어왔는지 확인
    }
    
}
