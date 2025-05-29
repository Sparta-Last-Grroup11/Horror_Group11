using UnityEngine;

public class NurseZombieChaseState : EnemyBaseState
{
    private NurseZombie nurseZombie;

    private float afterLastFootStep;

    public NurseZombieChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAgent.isStopped = false;
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", true);
        GameManager.Instance.player.isChased = true;
    }

    public override void Update()
    {
        // 플레이어가 좀비 자신을 보고 있거나, 조명이 켜졌거나, 플레이어를 놓쳤다면 대기 상태로 전환
        if (nurseZombie.IsPlayerLookingAtMe() || nurseZombie.lightStateSO.IsLightOn || enemy.HasLostPlayer())
        {
            fsm.ChangeState(nurseZombie.nurseZombieIdleState);
            return;
        }

        // 플레이어 가까이 있으며, 좀비 자신을 안 보고 있다면 
        if (IsNearPlayer() && !nurseZombie.IsPlayerLookingAtMe())
        {
            fsm.ChangeState(nurseZombie.nurseZombieAttackState);
            return;
        }

        nurseZombie.LookAtPlayer();
        MoveTowardsPlayer();
        CheckPlayerBeyondDoor();

    }

    public void MoveTowardsPlayer()  // NaveMesh 기반으로 플레이어를 향해 이동
    {
        if (nurseZombie.nurseZombieAgent == null || !nurseZombie.nurseZombieAgent.isOnNavMesh) return;

        nurseZombie.nurseZombieAgent.speed = nurseZombie.moveSpeed;
        nurseZombie.nurseZombieAgent.SetDestination(nurseZombie.PlayerTransform.position);

        afterLastFootStep += Time.deltaTime;

        if (afterLastFootStep >= nurseZombie.footStepRate)
        {
            afterLastFootStep = 0;
            AudioManager.Instance.Audio3DPlay(nurseZombie.chaseFootStepClip, nurseZombie.transform.position);
        }
    }

    public void CheckPlayerBeyondDoor()  // 플레이어를 추적하다가 문에 가로막힌 경우
    {
        Ray ray = new Ray(nurseZombie.transform.position + Vector3.up, nurseZombie.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, nurseZombie.detectDoorRange, nurseZombie.DoorLayerMask))
        {
            LockedDoor door = hit.collider.GetComponent<LockedDoor>();

            if (door != null && !door.IsOpened)
            {
                nurseZombie.blockedByDoorCount++;

                // 문에 막힌 횟수가 한도를 넘으면 좀비 비활성화
                if (nurseZombie.blockedByDoorCount >= nurseZombie.maxBlockedCount)
                {
                    nurseZombie.gameObject.SetActive(false);
                }

                // 재배치 및 상태 초기화
                nurseZombie.MoveToSpawnPosition(new Vector3(-5.96f, 5.5f, -19.71f), Quaternion.identity);
                fsm.ChangeState(nurseZombie.nurseZombieIdleState);
                nurseZombie.haveSeenPlayer = false;
                return;
            }
        }
    }
    
    public bool IsNearPlayer()  // 플레이어가 일정 거리 이내인지 확인 (공격 확인 여부)
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);
        return distance <= nurseZombie.attackRange;
    }
}