using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombie_ChaseState : E_BaseState
{
    private CopZombie copZombie;

    public CopZombie_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        copZombie.copzombieAgent.speed = 3f;
        Debug.Log("추격 시작");
    }

    public override void Update()
    {
        copZombie.copZombieAnim.SetFloat("MoveSpeed", copZombie.copzombieAgent.velocity.magnitude);
        copZombie.copzombieAgent.SetDestination(copZombie.PlayerTransform.position);
    }
}
