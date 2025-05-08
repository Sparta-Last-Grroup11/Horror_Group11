using UnityEngine;

public class NurseZombieIdleState : EnemyBaseState  // 기본 상태일 때
{
    private NurseZombie nurseZombie;

    public NurseZombieIdleState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        GameManager.Instance.player.isChased = false;
        nurseZombie.nurseZombieAgent.isStopped = true;
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", false);

    }

    public override void Update()
    {
        nurseZombie.CanSeePlayer();

        if (nurseZombie.haveSeenPlayer && !nurseZombie.IsPlayerLookingAtMe() && !nurseZombie.lightStateSO.IsLightOn)
        {
            fsm.ChangeState(nurseZombie.nurseZombieChaseState);
            enemy.FirstVisible(ref nurseZombie.hasBeenSeenByPlayer, nurseZombie.firstMonologueNum);
        }
    }
}
