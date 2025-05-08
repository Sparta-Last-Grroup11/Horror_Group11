using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopZombiePatrolState : EnemyBaseState
{
    private CopZombie copZombie;

    private float afterSetPoint;

    public CopZombiePatrolState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        copZombie.target = copZombie.transform;
    }

    public override void Update()
    {
        // 순찰 방식 및 상태 변환 조건
        copZombie.copZombieAnim.SetFloat("MoveSpeed", copZombie.copzombieAgent.velocity.magnitude);
        Patrol();

        if (enemy.CanSeePlayer())
        {
            fsm.ChangeState(copZombie.copZombieChaseState);
        }
    }

    private void Patrol()
    {
        afterSetPoint += Time.deltaTime;

        // 목적지 도착했으면 애니메이션 정지
        if (ReachedDestination())
        {
            copZombie.copZombieAnim.SetFloat("MoveSpeed", 0);
            copZombie.viewAngle = 120f;
        }
        else
        {
            copZombie.afterLastFootStep += Time.deltaTime;
            if (copZombie.afterLastFootStep > copZombie.footStepRate)
            {
                AudioManager.Instance.Audio3DPlay(copZombie.copZombieFootStep, copZombie.transform.position);
                copZombie.afterLastFootStep = 0;
            }
        }

        // 시간 지나면 새로운 목적지 설정
        if (afterSetPoint > copZombie.setPointRate)
        {
            Vector3 randomPosition = GetRandomPositionOnNavMesh();
            copZombie.copzombieAgent.SetDestination(randomPosition);
            copZombie.viewAngle = 90f;
            afterSetPoint = 0f;
        }
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * copZombie.patrolRange;
        randomDirection += copZombie.transform.position; // 랜덤 방향 벡터를 현재 위치에 더합니다.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인합니다.
        {
            return hit.position; // NavMesh 위의 랜덤 위치를 반환합니다.
        }
        else
        {
            return copZombie.transform.position; // NavMesh 위의 랜덤 위치를 찾지 못한 경우 현재 위치를 반환합니다.
        }
    }

    private bool ReachedDestination()
    {
        return !copZombie.copzombieAgent.pathPending &&
               copZombie.copzombieAgent.remainingDistance <= copZombie.copzombieAgent.stoppingDistance &&
               (!copZombie.copzombieAgent.hasPath || copZombie.copzombieAgent.velocity.sqrMagnitude <= 0.2f);
    }
}
