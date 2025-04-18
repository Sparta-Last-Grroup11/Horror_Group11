using System.Collections;
using UnityEngine;

public class SkinLessZombie_AmbushState : EnemyBaseState
{
    private SkinLessZombie skinLessZombie;

    private bool hasTriggered = false;  // 플레이어에게 달려들기 시작했는지 여부
    //public float timer = 0f;  // 달려든 후 일정 시간 지나면 사라지게 만들 타이머
    //public float rushSpeed = 30f;         // 달려드는 속도
    //public float disappearTime = 1.5f;    // 사라지기까지 시간
    //public float rushDelay = 0.5f; // 달려들기 전에 대기하는 시간

    public SkinLessZombie_AmbushState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (!hasTriggered && enemy.CanSeePlayer())
        {
            Debug.Log("ss");
            skinLessZombie.LookAtPlayer();
            skinLessZombie.StartCoroutine(StartRushDelay(skinLessZombie.rushDelay));
        }

        if (hasTriggered)
        {
            Vector3 direction = (skinLessZombie.PlayerTransform.position - skinLessZombie.transform.position).normalized;
            direction.y = 0;
            skinLessZombie.transform.position += direction * skinLessZombie.rushSpeed * Time.deltaTime;

            skinLessZombie.timer += Time.deltaTime;
            if (skinLessZombie.timer > skinLessZombie.disappearTime)
            {
                skinLessZombie.gameObject.SetActive(false);
            }
        }

    }

    private IEnumerator StartRushDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasTriggered = true;
    }

}
