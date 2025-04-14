using UnityEngine;

public class NurseZombie_ChaseState : E_BaseState    // 플레이어를 추격하는 상태일 때
{
    public NurseZombie nurseZombie;
    //private float chaseTimer;
    //private const float maxChaseTime = 5f;

    public NurseZombie_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        // chaseTimer = 0f;
        nurseZombie.nurseAnimator.SetBool("IsChasing", true);
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return;

        Vector3 direction = (nurseZombie.PlayerTransform.position - nurseZombie.transform.position).normalized;  // 플레이어 방향으로 이동    
        direction.y = 0;  // y축 방향은 무시
        nurseZombie.transform.position += direction * nurseZombie.moveSpeed * Time.deltaTime;  // 플레이어 쪽으로 이동

        if (nurseZombie.IsPlayerLookingAtMe())  // 플레이어와 마주보고 있을 때 
        {
            // 이 시점에 글리치 효과를 넣어주면 좋을 듯 합니다.
            fsm.ChangeState(new NurseZombie_IdleState(nurseZombie, fsm));
            return;
        }

        if (nurseZombie.IsNearPlayer())  // 천사가 일정 거리 안에 있다면
        {
            fsm.ChangeState(new NurseZombie_AttackState(nurseZombie, fsm));  // 공격 상태로 전환
            return;
        }

        //if (angel.IsLightOn())  // 불이 켜진 곳에서는 추적하지 않음
        //    return;

        //if (angel.IsPlayerInRoom())  // 플레이어가 방 안에서 대기중일 때
        //{
        //    chaseTimer += Time.deltaTime;

        //    if (chaseTimer >= maxChaseTime)  // chaseTimer가 일정 시간 이상 지나면 
        //    {
        //        fsm.ChangeState(new Angel_IdleState(angel, fsm));  // 추격 상태 풀리고 Idle 상태로 전환 (여기서 다른 곳으로 스폰? 아니면 그 자리 유지?)
        //        return;
        //    }
        //}

    }
}
