using UnityEngine;
using System.Collections;

public class NurseZombieAttackState : EnemyBaseState
{
    private NurseZombie nurseZombie;

    public NurseZombieAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        AudioManager.Instance.Audio2DPlay(nurseZombie.nurseZombieCatchPlayerClip, 1f, false, EAudioType.SFX);
        GameManager.Instance.player.isChased = false;
        GameManager.Instance.player.cantMove = true;
        nurseZombie.nurseZombieAnim.SetBool("Attack", true);
        nurseZombie.nurseZombieVirtualCamera.Priority = 12;
        GameManager.Instance.player.isDead = true;
        nurseZombie.StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1f);

        // 남은 목숨에 따라 다른 엔딩 연출
        if (GameManager.Instance.Life < 1)
        {
            UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.NoLife, 2000);
        }
        else
        {
            UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.Death, 2000);
        }

    }
}
