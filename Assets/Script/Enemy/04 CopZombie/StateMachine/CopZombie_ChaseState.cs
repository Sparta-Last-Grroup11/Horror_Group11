using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombie_ChaseState : EnemyBaseState
{
    private CopZombie copZombie;
    private float afterPlayerDisappear;
    private float detectPlayerRate = 5f;

    public CopZombie_ChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        copZombie.copzombieAgent.speed = 3f;
        Debug.Log("추격 시작");
        AudioManager.Instance.Audio3DPlay(copZombie.copZombieChaseClip, copZombie.transform.position);
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
        if (Vector3.Distance(GameManager.Instance.player.transform.position, copZombie.transform.position) < 1.8f)
        {
            fsm.ChangeState(new CopZombie_AttackState(copZombie, fsm));
        }
        if (copZombie.HasLostPlayer())
        {
            fsm.ChangeState(new CopZombie_PatrolState(copZombie, fsm));
        }
    }

    public override void Exit()
    {
        copZombie.copzombieAgent.speed = 1.5f;
        GameManager.Instance.player.isChased = false;
    }
}
