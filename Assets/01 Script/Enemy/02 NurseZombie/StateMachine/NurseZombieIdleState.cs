using System.Diagnostics;
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
        nurseZombie.canAttack = true;
        nurseZombie.attackRange = 1f;
        nurseZombie.nurseZombieAnim.ResetTrigger("Attack");
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", false);
        GameManager.Instance.player.isChased = false;
    }

    public override void Update()
    {
        if (nurseZombie.IsPlayerLookingAtMe())
        {
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
            return;
        }

        if (nurseZombie.CanSeePlayer() && !nurseZombie.IsPlayerLookingAtMe() && !nurseZombie.lightStateSO.IsLightOn)
        {
            fsm.ChangeState(new NurseZombieChaseState(nurseZombie, fsm));
            enemy.FirstVisible(ref nurseZombie.hasBeenSeenByPlayer, nurseZombie.firstMonologueNum);
        }
    }

}
