using UnityEngine;
using System.Collections;

public class NurseZombieAttackState : EnemyBaseState  // 플레이어를 공격하는 상태
{
    private NurseZombie nurseZombie;

    public NurseZombieAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.hasDashed = false;
        nurseZombie.StartCoroutine(AttackRoutine());
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return;

        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);

        if (!nurseZombie.hasDashed && distance > nurseZombie.dashTriggerRange)
        {
            nurseZombie.MoveTowardsPlayer(nurseZombie.dashSpeed);
        }
    }

    private IEnumerator AttackRoutine()
    {
        nurseZombie.nurseZombieAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        nurseZombie.hasDashed = true;
        EndAttack();

    }

    private void EndAttack()
    {
        GameManager.Instance.player.cantMove = true;
        var dyingUI = UIManager.Instance.show<DyingUI>();
        Debug.Log(dyingUI != null ? "DyingUI 호출 성공" : "DyingUI 로딩 실패");
    }
}
