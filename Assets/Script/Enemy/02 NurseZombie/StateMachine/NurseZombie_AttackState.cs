using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class NurseZombie_AttackState : EnemyBaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    // Dash
    private float attackRange = 0.5f; // 공격 범위
    private float dashSpeed = 8f; // 돌진 속도, 일반 추격보다 빠르게
    private bool hasDashed = false; // 돌진 완료 여부

    private GlitchUI glitchUI;
    private HeartBeat heartBeat;

    public NurseZombie_AttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        hasDashed = false;
        nurseZombie.StartCoroutine(AttackRoutine());
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return;

        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);

        if (!hasDashed && distance > attackRange)
        {
            nurseZombie.MoveTowardsPlayer(dashSpeed);
        }
    }

    private IEnumerator AttackRoutine()
    {
        nurseZombie.nurseZombieAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        hasDashed = true;
        EndAttack();
    }

    private void EndAttack()
    {
        glitchUI.GlitchEnd();
        heartBeat.ChanbeatSpeed(0f);
        heartBeat.gameObject.SetActive(false);

        Canvas dyingUI = Resources.Load<Canvas>("UI/DyingUI");
        if (dyingUI != null)
        {
            Canvas instantiatedUI = Instantiate(dyingUI);
            instantiatedUI.gameObject.SetActive(true);  // UI 활성화
            DyingUI dyingScript = instantiatedUI.GetComponent<DyingUI>();
            if (dyingScript != null)
            {
                UIManager.Instance.ClearListAndDestroy(dyingScript);  // DyingUI가 UIManager에서 관리된다면 제거
            }
        }

    }

}
