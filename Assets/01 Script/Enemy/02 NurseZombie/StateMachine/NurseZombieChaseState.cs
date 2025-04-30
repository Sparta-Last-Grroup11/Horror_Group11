using UnityEngine;

public class NurseZombieChaseState : EnemyBaseState    // 플레이어를 추격하는 상태일 때
{
    public NurseZombie nurseZombie;

    public NurseZombieChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", false);
        AudioManager.Instance.Audio2DPlay(nurseZombie.nurseZombieChaseClip, 10f);
        GameManager.Instance.player.isChased = true;
        nurseZombie.waitTimer = 0f;

    }

    public override void Update()
    {
        if (HandleLightOn()) return;
        if (HandleLostPlayer()) return;

        UpdatePlayerSightInfo();
        CheckBackTurnTrigger();
        TryStartChase();
        CheckIfPlayerInRoom();
        TransitionToAttack();

    }

    private bool HandleLightOn()
    {
        if (nurseZombie.lightStateSO.IsLightOn)
        {
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
            return true;
        }
        return false;
    }

    private bool HandleLostPlayer()
    {
        if (!nurseZombie.hasDetectedPlayer && enemy.HasLostPlayer())
        {
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
            return true;
        }
        return false;
    }

    private void UpdatePlayerSightInfo()
    {
        bool isLookingnow = nurseZombie.IsPlayerLookingAtMe();
        nurseZombie.wasLookedByPlayer = isLookingnow;
        nurseZombie.FirstVisible(ref nurseZombie.hasBeenSeenByPlayer, nurseZombie.firstMonologueNum);
    }

    private void CheckBackTurnTrigger()
    {
        bool isLookingnow = nurseZombie.IsPlayerLookingAtMe();
        bool canSeePlayer = nurseZombie.CanSeePlayer();

        if (!nurseZombie.isTriggeredByBackTurn &&
            nurseZombie.wasLookedByPlayer &&
            !isLookingnow &&
            canSeePlayer)
        {
            nurseZombie.isTriggeredByBackTurn = true;
        }
    }

    private void TryStartChase()
    {
        if (!nurseZombie.isTriggeredByBackTurn) return;
        {
            nurseZombie.nurseZombieAnim.SetBool("IsChasing", true);
            nurseZombie.LookAtPlayer();
            enemy.MoveTowardsPlayer(nurseZombie.moveSpeed);
        }
    }

    private void CheckIfPlayerInRoom()
    {
        nurseZombie.afterDetectDoor += Time.deltaTime;
        if (nurseZombie.afterDetectDoor >= nurseZombie.detectDoorRate)   // 문 닫힘 감지하면 Wait상태로 전환
        {
            Ray ray = new Ray(nurseZombie.transform.position + Vector3.up, nurseZombie.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, nurseZombie.detectDoorRange, nurseZombie.DoorLayerMask))
            {
                GameObject doorObj = hit.collider.gameObject;
                if (doorObj.activeInHierarchy)
                {
                    Debug.Log("방 안에 있음");
                    HandlePlayerInRoom();
                    return;
                }
            }
            nurseZombie.afterDetectDoor = 0;
        }
    }

    private void HandlePlayerInRoom()
    {
        nurseZombie.waitTimer += Time.deltaTime;
        if (nurseZombie.waitTimer >= nurseZombie.PlayerDisappearTime)  // 방 밖에서 일정 시간 대기 후 스폰 위치로 이동, 다시 IdleState로 전환
        {
            nurseZombie.hasDetectedPlayer = false;
            fsm.ChangeState(new NurseZombieIdleState(nurseZombie, fsm));
        }
        return;
    }

    private void TransitionToAttack()
    {
        if (IsNearPlayer())  // 천사가 일정 거리 안에 있다면 Attack 상태로 전환
        {
            fsm.ChangeState(new NurseZombieAttackState(nurseZombie, fsm));
        }
    }

    private bool IsNearPlayer()
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= nurseZombie.attackRange;  // 공격 범위 안에 들어왔는지 확인
    }
}