using UnityEngine;

public class NurseZombie_AttackState : EnemyBaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    private float attackRange = 0.5f; // 공격 범위
    private float dashSpeed = 8f; // 돌진 속도, 일반 추격보다 빠르게
    private bool hasDashed = false; // 돌진 완료 여부

    public NurseZombie_AttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        hasDashed = false;
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return; 

        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);

        if (!hasDashed)
        {
            if (distance > attackRange)
            {
                nurseZombie.MoveTowardsPlayer(dashSpeed);
            }
            else if (distance <= attackRange && distance > 0.2f)
            {
                nurseZombie.MoveTowardsPlayer(dashSpeed * 0.5f);
            }
            else
            {
                hasDashed = true;
                nurseZombie.nurseZombieAnim.SetTrigger("Attack"); 
            }
        }
        else
        {
            if (FinishAttack())
            {
                EndAttack(); // 글리치 종료하고 GameOver UI 띄우기
            }
        }
    }

    public bool FinishAttack()  // 공격 애니메이션이 끝났는지 확인
    {
        AnimatorStateInfo stateInfo = nurseZombie.nurseZombieAnim.GetCurrentAnimatorStateInfo(0);
        return !(stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f);
    }

    private void EndAttack()
    {
        GlitchUI glitchUI = UIManager.Instance.Get<GlitchUI>();
        if (glitchUI != null)
        {
            glitchUI.GlitchEnd();
        }

        HeartBeat heartBeat = UIManager.Instance.Get<HeartBeat>(); 
        if (heartBeat != null)
        {
            heartBeat.ChanbeatSpeed(0f);
            heartBeat.gameObject.SetActive(false);
        }

        // GameOverUi 띄우고 GameOver 처리
    }

}
