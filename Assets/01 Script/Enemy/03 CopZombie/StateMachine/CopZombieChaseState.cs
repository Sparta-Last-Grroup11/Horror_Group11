using UnityEngine;

public class CopZombieChaseState : EnemyBaseState
{
    private CopZombie copZombie;

    public CopZombieChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        copZombie.copzombieAgent.speed = 3f;
        AudioManager.Instance.Audio2DPlay(copZombie.copZombieChaseClip,1, false, EAudioType.SFX);
        GameManager.Instance.player.isChased = true;
    }

    public override void Update()
    {
        copZombie.copZombieAnim.SetFloat("MoveSpeed", copZombie.copzombieAgent.velocity.magnitude);
        copZombie.copzombieAgent.SetDestination(copZombie.PlayerTransform.position);
        copZombie.afterLastFootStep += Time.deltaTime;
        if (copZombie.afterLastFootStep > copZombie.footStepRate / 2)
        {
            AudioManager.Instance.Audio3DPlay(copZombie.copZombieFootStep, copZombie.transform.position);
            copZombie.afterLastFootStep = 0;
        }
        if (Vector3.Distance(GameManager.Instance.player.transform.position, copZombie.transform.position) < 1.5f)
        {
            fsm.ChangeState(copZombie.copZombieAttackState);
        }
        if (copZombie.HasLostPlayer())
        {
            fsm.ChangeState(copZombie.copZombiePatrolState);
        }
    }

    public override void Exit()
    {
        copZombie.copzombieAgent.speed = 1.5f;
        GameManager.Instance.player.isChased = false;
    }
}
