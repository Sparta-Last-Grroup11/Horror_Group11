using UnityEngine;

public class NurseZombie_ChaseState : E_BaseState    // 플레이어를 추격하는 상태일 때
{
    public NurseZombie nurseZombie;

    private float PlayerDisappearTime = 3.0f;
    private float waitTimer = 0f;

    public NurseZombie_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", true);
        waitTimer = 0f;
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return;

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
                    PlayerInRoom();
                    return;
                }
            }
            nurseZombie.afterDetectDoor = 0;
        }

        if (nurseZombie.IsPlayerLookingAtMe())  // 플레이어와 마주보고 있을 때 Idle 상태로 전환
        {
            fsm.ChangeState(new NurseZombie_IdleState(nurseZombie, fsm));
            return;
        }

        if (IsNearPlayer())  // 천사가 일정 거리 안에 있다면 Attack 상태로 전환
        {
            fsm.ChangeState(new NurseZombie_AttackState(nurseZombie, fsm));
            return;
        }

        nurseZombie.MoveTowardsPlayer(nurseZombie.moveSpeed);  // 플레이어 방향대로 움직임

    }

    public void PlayerInRoom()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= PlayerDisappearTime)  // 방 밖에서 일정 시간 대기 후 스폰 위치로 이동, 다시 IdleState로 전환
        {
            MoveToSpawnPosition();
            fsm.ChangeState(new NurseZombie_IdleState(nurseZombie, fsm));
        }
        return;
    }

    private void MoveToSpawnPosition() 
    {
        // 여기에 스폰위치를 가져와주면 될듯.
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= nurseZombie.attackRange;  // 공격 범위 안에 들어왔는지 확인
    }


}
