using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CopZombie_AttackState : E_BaseState
{
    private CopZombie copZombie;

    public CopZombie_AttackState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        GameManager.Instance.player.stateMachine.ChangeState(new PlayerCaughtState(GameManager.Instance.player));
        copZombie.copZombieVirtualCamera.Priority = 12;
        copZombie.playableDirector.Play();
        copZombie.copZombieAnim.SetTrigger("DoAttack");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}
