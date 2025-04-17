using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombie_AttackState : EnemyBaseState
{
    private CopZombie copZombie;

    public CopZombie_AttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
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
