using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLess_PatrolState : E_BaseState
{
    private SkinLess skinLess;
    private Transform[] wayPoints; 
    private int currentIndex = 0;

    public SkinLess_PatrolState(Enemy enemy, E_StateMachine fsm, Transform[] patrolPoints) : base(enemy, fsm)
    {
        skinLess = enemy as SkinLess;
        wayPoints = patrolPoints;
    }

    public override void Enter()
    {
        skinLess.Agent.speed = skinLess.patrolSpeed;
        skinLess.MoveTo(wayPoints[currentIndex].position);
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer())  // 플레이어가 처음 시야각에 들어올 때 
        {
            fsm.ChangeState(new Nurse_ChaseState(skinLess, fsm));  // 추적Chase 상태로 전환
            return;
        }

        if (skinLess.Agent.remainingDistance < 0.2f)  // 목표 지점에 거의 도착했으면
        {
            currentIndex = (currentIndex + 1) % wayPoints.Length;
            skinLess.MoveTo(wayPoints[currentIndex].position);
        }


    }
}
