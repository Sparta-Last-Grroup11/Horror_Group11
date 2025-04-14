using UnityEngine;

public class NurseZombie_IdleState : E_BaseState  // 기본 상태일 때
{
    private NurseZombie nurse;

    public NurseZombie_IdleState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurse = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurse.nurseAnimator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer() && !nurse.IsPlayerLookingAtMe())  // 1) 플레이어가 처음 시야각에 들어오고, 2) 플레이어가 날 보지 않을 때 
        {
            fsm.ChangeState(new NurseZombie_ChaseState(nurse, fsm));  // 추적Chase 상태로 전환
            Debug.Log("추적 왜 안 돼?");
        }
    }


}
