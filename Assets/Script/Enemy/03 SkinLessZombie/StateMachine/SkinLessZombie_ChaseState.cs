using UnityEngine;

public class SkinLessZombie_ChaseState : E_BaseState
{
    private SkinLessZombie skinLessZombie;
    private float losePlayerTimer = 0f;
    private float loseThreshold = 5f;  // 5초 동안 안 보이면 포기

    public SkinLessZombie_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        skinLessZombie.Agent.speed = skinLessZombie.chaseSpeed;
        skinLessZombie.SkinLessAnimator.speed = 1.5f;
        skinLessZombie.Agent.isStopped = false;
    }

    public override void Update()
    {
        if (skinLessZombie.CanSeePlayer())
        {
            skinLessZombie.Agent.SetDestination(skinLessZombie.PlayerTransform.position);
            losePlayerTimer = 0f;  // 타이머 초기화
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer >= loseThreshold)
            {
                fsm.ChangeState(new SkinLessZombie_ReturnState(skinLessZombie, fsm));
            }
        }

    }
}
