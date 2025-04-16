using UnityEngine;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    public Animator nurseZombieAnim { get; private set; }
    public Rigidbody rb;
    public float moveSpeed = 4f;  // 이동 속도
    public float attackRange = 2f;  // 공격 범위

    protected override void Start()
    {
        base.Start();
        nurseZombieAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Collider nurseCollider = GetComponent<Collider>();
        Collider playerCollider = PlayerTransform.GetComponent<Collider>();
        if (nurseCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(nurseCollider, playerCollider);
        }

        InitNurseFSM();
    }

    private void InitNurseFSM()
    {
        fsm = new E_StateMachine();
        fsm.ChangeState(new NurseZombie_IdleState(this, fsm));
    }

    //protected override void Update()
    //{
    //    base.Update();
    //}

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - PlayerTransform.position).normalized;  // 플레이어에서 몬스터를 향하는 방향 벡터
        Vector3 playerforward = PlayerTransform.forward.normalized;  // 플레이어가 보고 있는 방향 벡터

        float dot = Vector3.Dot(toNurse, playerforward);  // 1에 가까울수록 플레이어 = 몬스터 같은 방향
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
    }

    public void MoveTowardsPlayer(float speed)
    {
        Vector3 direction = (PlayerTransform.position - transform.position).normalized;
        direction.y = 0;
        float distance = Vector3.Distance(transform.position, PlayerTransform.position);
        float minDistance = 1.0f;  //  플레이어와 최소 거리 유지
        if (distance > minDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(transform.position, PlayerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= attackRange;  // 공격 범위 안에 들어왔는지 확인
    }

    public bool FinishAttack()  // 공격 애니메이션이 끝났는지 확인하는 메서드
    {
        AnimatorStateInfo stateInfo = nurseZombieAnim.GetCurrentAnimatorStateInfo(0);
        return !(stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f);
    }

}
