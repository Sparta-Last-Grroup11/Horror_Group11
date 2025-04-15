using System.Collections;
using System.Collections.Generic;
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
        copZombie.copZombieAnim.SetTrigger("DoAttack");
    }

    public override void Update()
    {
        
    }
}
