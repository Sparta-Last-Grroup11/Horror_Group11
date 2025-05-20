using System.Collections;
using UnityEngine;

public class CopZombieAttackState : EnemyBaseState
{
    private CopZombie copZombie;

    public CopZombieAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        GameManager.Instance.player.cantMove = true;
        copZombie.copZombieVirtualCamera.Priority = 12;
        copZombie.copZombieAnim.SetTrigger("DoAttack");
        AudioManager.Instance.Audio2DPlay(copZombie.copZombieCatchBGMClip, 1f, false, EAudioType.SFX);
        copZombie.StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(5f);
        UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.Death, 1000);
    }
}
