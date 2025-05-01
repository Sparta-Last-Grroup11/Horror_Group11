using UnityEngine;

public class NurseZombieChaseState : EnemyBaseState    // 플레이어를 추격하는 상태일 때
{
    private NurseZombie nurseZombie;

    public NurseZombieChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        GameManager.Instance.player.isChased = true;
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", true);
        nurseZombie.hasDetectedPlayer = true;
        AudioManager.Instance.Audio2DPlay(nurseZombie.nurseZombieChaseClip, 10f);
        nurseZombie.waitTimer = 0f;
    }

    public override void Update()
    {
        if (ShouldReturnToIdle()) return;

        if (nurseZombie.IsPlayerLookingAtMe())
        {
            nurseZombie.nurseZombieAgent.isStopped = true;
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
            return;
        }
        else
        {
            nurseZombie.nurseZombieAgent.isStopped = false;
        }

        nurseZombie.LookAtPlayer();
        nurseZombie.MoveTowardsPlayer(nurseZombie.moveSpeed);

        CheckIfPlayerInRoom();
        TransitionToAttack();
        
    }

    private bool ShouldReturnToIdle()
    {
        if (nurseZombie.lightStateSO.IsLightOn)
        {
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
            return true;
        }

        if (!nurseZombie.hasDetectedPlayer && enemy.HasLostPlayer())
        {
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
            return true;
        }

        return false;
    }

    public void CheckIfPlayerInRoom()
    {
        Ray ray = new Ray(nurseZombie.transform.position + Vector3.up, nurseZombie.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, nurseZombie.detectDoorRange, nurseZombie.DoorLayerMask))
        {
            LockedDoor door = hit.collider.GetComponent<LockedDoor>();
            if (door != null && !door.IsOpened)
            {
                Debug.Log("LockedDoor 발견, Idle 전환");
                nurseZombie.hasDetectedPlayer = false;
                nurseZombie.MoveToSpawnPosition(new Vector3(-7f, 5.5f, 12.7f));
                fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
                return;
            }
        }
    }

    public void TransitionToAttack()
    {
        if (nurseZombie.IsPlayerLookingAtMe()) return;

        Debug.Log("TransitionToAttack() 진입");
        if (IsNearPlayer())  // 천사가 일정 거리 안에 있다면 Attack 상태로 전환
        {
            Debug.Log("Attack 상태 전환!");
            fsm.ChangeState(new NurseZombieAttackState(nurseZombie, fsm));
        }
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= nurseZombie.attackRange;  // 공격 범위 안에 들어왔는지 확인
    }
}