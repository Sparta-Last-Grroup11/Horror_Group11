using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombie_ChaseState : E_BaseState
{
    private CopZombie copZombie;
    private float afterPlayerDisappear;
    private float detectPlayerRate = 5f;

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
        if (PlayerDisappear())
        {
            fsm.ChangeState(new CopZombie_PatrolState(copZombie, fsm));
        }
    }

    private bool PlayerDisappear()
    {
        if (copZombie.CanSeePlayer())
        {
            afterPlayerDisappear = 0;
            return false;
        }
        else
        {
            //Debug.Log("플레이어 사라짐");
            afterPlayerDisappear += Time.deltaTime;
            if (afterPlayerDisappear > detectPlayerRate)
            {
                return true;
            }
            return false;
        }
    }
}
