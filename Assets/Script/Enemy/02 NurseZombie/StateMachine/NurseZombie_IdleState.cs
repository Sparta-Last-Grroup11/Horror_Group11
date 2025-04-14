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
        nurseZombie.nurseAnimator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        Debug.Log("IdleState: Update 진입");

        bool canSee = enemy.CanSeePlayer();
        bool isPlayerLooking = nurseZombie.IsPlayerLookingAtMe();

        Debug.Log($"CanSeePlayer: {canSee}, IsPlayerLookingAtMe: {isPlayerLooking}");

        if (canSee && !isPlayerLooking)
        {
            Debug.Log("추적상태로 전환");
            fsm.ChangeState(new NurseZombie_ChaseState(nurseZombie, fsm));
        }
    }
}
