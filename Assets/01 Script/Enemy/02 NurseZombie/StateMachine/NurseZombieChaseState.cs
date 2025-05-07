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
        nurseZombie.nurseZombieAgent.isStopped = false;
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", true);
        GameManager.Instance.player.isChased = true;

        if (!nurseZombie.hasPlayedChaseSound)
        {
            AudioManager.Instance.Audio2DPlay(nurseZombie.nurseZombieChaseClip, 1f);

            nurseZombie.hasPlayedChaseSound = true;
        }
    }

    public override void Update()
    {
        if (nurseZombie.IsPlayerLookingAtMe() || nurseZombie.lightStateSO.IsLightOn || enemy.HasLostPlayer())
        {
            fsm.ChangeState(nurseZombie.nurseZombieIdleState);
            return;
        }

        if (IsNearPlayer() && !nurseZombie.IsPlayerLookingAtMe()) // 천사가 일정 거리 안에 있다면 Attack 상태로 전환
        {
            nurseZombie.attackReadyTimer += Time.deltaTime;
            if (nurseZombie.attackReadyTimer > nurseZombie.requiredHoldTime)
            {
                fsm.ChangeState(nurseZombie.nurseZombieAttackState);
                return;
            }
        }
        else
        {
            nurseZombie.attackReadyTimer = 0f;
        }

        nurseZombie.LookAtPlayer();
        MoveTowardsPlayer();
        CheckPlayerBeyondDoor();

    }

    public void MoveTowardsPlayer()
    {
        if (nurseZombie.nurseZombieAgent == null || !nurseZombie.nurseZombieAgent.isOnNavMesh) return;

        nurseZombie.nurseZombieAgent.speed = nurseZombie.moveSpeed;
        nurseZombie.nurseZombieAgent.SetDestination(nurseZombie.PlayerTransform.position);
    }

    public void CheckPlayerBeyondDoor()
    {
        Ray ray = new Ray(nurseZombie.transform.position + Vector3.up, nurseZombie.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, nurseZombie.detectDoorRange, nurseZombie.DoorLayerMask))
        {
            LockedDoor door = hit.collider.GetComponent<LockedDoor>();
            if (door != null && !door.IsOpened)
            {
                Debug.Log("LockedDoor 발견, Idle 전환");
                nurseZombie.MoveToSpawnPosition(new Vector3(-7f, 5.5f, 12.7f));
                fsm.ChangeState(nurseZombie.nurseZombieIdleState);
                nurseZombie.haveSeenPlayer = false;
                return;
            }
        }
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= nurseZombie.attackRange;  // 공격 범위 안에 들어왔는지 확인
    }

}