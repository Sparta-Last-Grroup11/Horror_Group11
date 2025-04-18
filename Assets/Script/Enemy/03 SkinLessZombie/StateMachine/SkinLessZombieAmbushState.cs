using System.Collections;
using UnityEngine;

public class SkinLessZombieAmbushState : EnemyBaseState
{
    private SkinLessZombie skinLessZombie;


    private bool hasTriggered = false;  // 플레이어에게 달려들기 시작했는지 여부

    public SkinLessZombieAmbushState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        base.Enter();
        //GameManager.Instance.player.isChased = true;
    }

    public override void Update()
    {
        if (!hasTriggered && enemy.CanSeePlayer())
        {
            skinLessZombie.LookAtPlayer();
            skinLessZombie.skinLessZombieAnim.SetTrigger("Chase");
            skinLessZombie.StartCoroutine(StartRushDelay(skinLessZombie.rushDelay));
        }

        if (hasTriggered)
        {
            skinLessZombie.LookAtPlayer();
            Vector3 direction = (skinLessZombie.PlayerTransform.position - skinLessZombie.transform.position).normalized;
            direction.y = 0;
            skinLessZombie.rigidbody.MovePosition(skinLessZombie.transform.position + direction * skinLessZombie.rushSpeed * Time.deltaTime);
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

    public override void Exit()
    {
        //GameManager.Instance.player.isChased = false;
    }


}
