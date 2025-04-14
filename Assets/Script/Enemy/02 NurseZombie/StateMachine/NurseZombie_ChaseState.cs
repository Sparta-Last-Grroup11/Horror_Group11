using UnityEngine;

public class NurseZombie_ChaseState : E_BaseState    // 플레이어를 추격하는 상태일 때
{
    public NurseZombie nurseZombie;

    public NurseZombie_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        Debug.Log("왜 안 되지?");
        nurseZombie.nurseAnimator.SetBool("IsChasing", true);
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return;

        nurseZombie.MoveTowardsPlayer(nurseZombie.moveSpeed);

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

    }
}
