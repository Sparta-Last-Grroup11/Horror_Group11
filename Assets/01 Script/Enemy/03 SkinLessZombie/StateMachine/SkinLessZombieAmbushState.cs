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
        // UIManager.Instance.Get<GlitchUI>().GlitchStart(10f);
        skinLessZombie.skinLessZombieAnim.SetTrigger("Chase");
        skinLessZombie.FirstVisible(ref skinLessZombie.hasBeenSeenByPlayer, skinLessZombie.firstMonologueNum);
        AudioManager.Instance.Audio2DPlay(skinLessZombie.spottedRoarClip, 1f);

        Vector3 target = enemy.PlayerTransform.position;
        Vector3 direction = (target - skinLessZombie.transform.position).normalized;
        skinLessZombie.rigidbody.MovePosition(skinLessZombie.transform.position + direction * skinLessZombie.rushSpeed * Time.deltaTime);
        AudioManager.Instance.Audio2DPlay(skinLessZombie.rushFootstepsLoop, 0.8f);
    }

    public override void Update()
    {
        skinLessZombie.LookAtPlayer();
        Vector3 direction = (skinLessZombie.PlayerTransform.position - skinLessZombie.transform.position).normalized;
        skinLessZombie.rigidbody.MovePosition(skinLessZombie.transform.position + direction * skinLessZombie.rushSpeed * Time.deltaTime);

        float distance = Vector3.Distance(skinLessZombie.transform.position, skinLessZombie.PlayerTransform.position);
        if (distance < 1.0f)
        {
            GameObject.Destroy(skinLessZombie.gameObject);
            // UIMananger.Instance.Get<GlithUI>().GlitchEnd(0f);
            GameManager.Instance.player.isChased = false;
        }
    }
}
