using UnityEngine;

public class Nurse_IdleState : E_BaseState  // 기본 상태일 때
{
    private Nurse nurse;

    public Nurse_IdleState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurse = enemy as Nurse;
    }

    public override void Enter()
    {
        nurse.nurseAnimator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer() && !nurse.IsPlayerLookingAtMe())  // 1) 플레이어가 처음 시야각에 들어오고, 2) 플레이어가 날 보지 않을 때 
        {
            fsm.ChangeState(new Nurse_ChaseState(nurse, fsm));  // 추적Chase 상태로 전환
            Debug.Log("추적 왜 안 돼?");
        }
    }


}
