using UnityEngine;

public class SkinLessZombie_PatrolState : E_BaseState
{
    private SkinLessZombie skinLessZombie;
    private Transform[] wayPoints; // 좀비의 이동 지점들
    private int currentIndex = 0;  // 처음 시작하는 위치

    public SkinLessZombie_PatrolState(Enemy enemy, E_StateMachine fsm, Transform[] patrolPoints, int startIndex) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
        wayPoints = patrolPoints;
        currentIndex = startIndex;
    }

    public override void Enter()
    {
        skinLessZombie.Agent.speed = skinLessZombie.patrolSpeed;
        skinLessZombie.MoveTo(wayPoints[currentIndex].position);
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer())  // 플레이어가 처음 시야각에 들어올 때 
        {
            fsm.ChangeState(new SkinLessZombie_ChaseState(skinLessZombie, fsm));  // 추적Chase 상태로 전환
            return;
        }

        if (skinLessZombie.Agent.remainingDistance < 0.2f)  // 목표 지점에 거의 도착했으면
        {
            currentIndex = (currentIndex + 1) % wayPoints.Length;
            skinLessZombie.MoveTo(wayPoints[currentIndex].position);
        }
    }
}
