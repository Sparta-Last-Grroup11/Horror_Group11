using UnityEngine;

public class SkinLessZombie_AmbushState : E_BaseState
{
    private SkinLessZombie skinLessZombie;
    private Transform player;

    private bool hasTriggered = false;  // 플레이어에게 달려들기 시작했는지 여부
    private float timer = 0f;  // 달려든 후 일정 시간 지나면 사라지게 만들 타이머
    private float detectionRange = 5f;     // 플레이어 감지 범위
    private float rushSpeed = 80f;         // 달려드는 속도
    private float disappearTime = 1.5f;    // 사라지기까지 시간

    public SkinLessZombie_AmbushState(SkinLessZombie enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        base.Enter();

        if (skinLessZombie.CanSeePlayer())
        {
            hasTriggered = true;
            skinLessZombie.Agent.enabled = true;
            skinLessZombie.Agent.speed = rushSpeed;
            skinLessZombie.Agent.SetDestination(skinLessZombie.PlayerTransform.position);

        }
        skinLessZombie.Agent.isStopped = false;
    }

    public override void Update()
    {
        if (hasTriggered)
        {
            timer += Time.deltaTime;
            if (timer > disappearTime)
            {
                skinLessZombie.gameObject.SetActive(false);
            }
        }

    }
}
