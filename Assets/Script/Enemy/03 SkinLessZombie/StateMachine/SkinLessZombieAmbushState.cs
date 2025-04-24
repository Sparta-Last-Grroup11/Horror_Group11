using System.Collections;
using UnityEngine;

public class SkinLessZombieAmbushState : EnemyBaseState
{
    private SkinLessZombie skinLessZombie;
    private bool hasTriggered = false;  // 플레이어에게 달려들기 시작했는지 여부
    private bool isGlitchOn;

    public SkinLessZombieAmbushState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
    }

    public override void Enter()
    {
        base.Enter();
        isGlitchOn = false;
    }

    public override void Update()
    {
        enemy.CheckDoorOpen();

        if (!hasTriggered && enemy.CanSeePlayer() && enemy.isDoorOpened)
        {
            GameManager.Instance.player.isChased = true;
            UIManager.Instance.Get<GlitchUI>().GlitchStart(10f);
            isGlitchOn = true;
            AudioManager.Instance.Audio2DPlay(skinLessZombie.spottedRoarClip, 1f); // 플레이어 발견 순간 포효
            skinLessZombie.LookAtPlayer();
            skinLessZombie.skinLessZombieAnim.SetTrigger("Chase");
            skinLessZombie.StartCoroutine(StartRushDelay(skinLessZombie.rushDelay));
        }

        if (hasTriggered)
        {
            skinLessZombie.LookAtPlayer();
            Vector3 target = skinLessZombie.cameraTransform.position;
            float yOffset = 0.5f;
            target.y -= yOffset;
            Vector3 direction = (target - skinLessZombie.transform.position).normalized;
            skinLessZombie.rigidbody.MovePosition(skinLessZombie.transform.position + direction * skinLessZombie.rushSpeed * Time.deltaTime);
            skinLessZombie.timer += Time.deltaTime;

            if (skinLessZombie.timer > skinLessZombie.disappearTime)
            {
                if (isGlitchOn)
                {
                    UIManager.Instance.Get<GlitchUI>().GlitchEnd();
                    isGlitchOn = false;
                }

                GameManager.Instance.player.isChased = false;
                AudioManager.Instance.Audio2DPlay(skinLessZombie.rushFootstepsLoop, 0f);
                skinLessZombie.gameObject.SetActive(false);
            }
        }

    }

    private IEnumerator StartRushDelay(float delay)
    {
        float elapsed = 0f;
        while (elapsed < delay)
        {
            skinLessZombie.LookAtPlayer();
            elapsed += Time.deltaTime;
            yield return null;
        }

        hasTriggered = true;
        AudioManager.Instance.Audio2DPlay(skinLessZombie.rushFootstepsLoop, 0.8f);

    }

}
