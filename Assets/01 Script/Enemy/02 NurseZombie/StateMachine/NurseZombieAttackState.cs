using UnityEngine;
using System.Collections;

public class NurseZombieAttackState : EnemyBaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    public NurseZombieAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        GameManager.Instance.player.isChased = false;
        GameManager.Instance.player.cantMove = true;
        nurseZombie.nurseZombieAnim.SetTrigger("Attack");
        nurseZombie.nurseZombieVirtualCamera.Priority = 12;
        nurseZombie.StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.Death);
    }
}
