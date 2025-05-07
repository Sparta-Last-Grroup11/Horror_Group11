using UnityEngine;

public class NurseZombieAttackState : EnemyBaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    public NurseZombieAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAnim.SetTrigger("Attack");
        GameManager.Instance.player.cantMove = true;
        UIManager.Instance.show<DyingUI>();
    }
}
