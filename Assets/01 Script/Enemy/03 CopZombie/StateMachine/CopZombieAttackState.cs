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
        GameManager.Instance.player.isDead = true;
        copZombie.StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(3f);

        if (GameManager.Instance.Life < 1)
        {
            UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.NoLife, 1000);
        }
        else
        {
            UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.Death, 1000);
        }
    }
}
