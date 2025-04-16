using UnityEngine;

public class NurseZombie_IdleState : E_BaseState  // 기본 상태일 때
{
    private NurseZombie nurseZombie;

    public NurseZombie_IdleState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        bool canSee = enemy.CanSeePlayer();
        bool isPlayerLooking = nurseZombie.IsPlayerLookingAtMe();

        if (canSee && !isPlayerLooking)
        {
            Debug.Log("추적상태로 전환");
            fsm.ChangeState(new NurseZombie_ChaseState(nurseZombie, fsm));
        }
    }
}
