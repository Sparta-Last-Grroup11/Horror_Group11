using System.Collections;
using UnityEngine;

public class SkinLessZombieAmbushState : EnemyBaseState
{
    private SkinLessZombie skinLessZombie;

    public SkinLessZombieAmbushState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.player.isChased = true;

        UIManager.Instance.Get<GlitchUI>().GlitchStart(10f);
        skinLessZombie.skinLessZombieAnim.SetTrigger("Chase");
        skinLessZombie.StartCoroutine(StartRushDelay(skinLessZombie.rushDelay));

        AudioManager.Instance.Audio2DPlay(skinLessZombie.spottedRoarClip, 1f);
       
    }

    private IEnumerator StartRushDelay(float delay)
    {
        float elapsed = 0f;
        while (elapsed < delay)
        {
            skinLessZombie.LookAtPlayer();
            elapsed += Time.deltaTime;
            yield return null;
        }

        Vector3 target = skinLessZombie.cameraTransform.position;
        target.y -= 0.5f;
        Vector3 direction = (target - skinLessZombie.transform.position).normalized;
        skinLessZombie.rigidbody.MovePosition(skinLessZombie.transform.position + direction * skinLessZombie.rushSpeed * Time.deltaTime);
        AudioManager.Instance.Audio2DPlay(skinLessZombie.rushFootstepsLoop, 0.8f);
        
    }
}
