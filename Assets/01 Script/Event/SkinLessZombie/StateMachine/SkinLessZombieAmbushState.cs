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
        UIManager.Instance.GlitchStart(10f);
        skinLessZombie.skinLessZombieAnim.SetTrigger("Chase");
        skinLessZombie.FirstVisible(ref skinLessZombie.hasBeenSeenByPlayer, skinLessZombie.firstMonologueNum);

        AudioManager.Instance.Audio2DPlay(skinLessZombie.spottedRoarClip, 1f);
        AudioManager.Instance.Audio2DPlay(skinLessZombie.rushFootstepsLoop, 1f);

        MoveTowardPlayer(3.0f);

    }

    public override void Update()
    {
        skinLessZombie.LookAtPlayer();
        MoveTowardPlayer(3.0f);

        float distance = Vector3.Distance(skinLessZombie.transform.position, skinLessZombie.PlayerTransform.position);
        if (distance < 1.0f)
        {
            GameObject.Destroy(skinLessZombie.gameObject);
            UIManager.Instance.GlitchEnd();
            GameManager.Instance.player.isChased = false;
        }
    }

    private void MoveTowardPlayer(float verticalOffset)
    {
        Vector3 target = enemy.PlayerTransform.position + Vector3.up * verticalOffset;
        Vector3 direction = (target - skinLessZombie.transform.position).normalized;
        skinLessZombie.GetComponent<Rigidbody>().MovePosition(skinLessZombie.transform.position + direction * skinLessZombie.rushSpeed * Time.deltaTime);
    }
}
