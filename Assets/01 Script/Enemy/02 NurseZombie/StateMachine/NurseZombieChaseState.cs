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
        if (nurseZombie.IsPlayerLookingAtMe() || nurseZombie.lightStateSO.IsLightOn || enemy.HasLostPlayer())
        {
            fsm.ChangeState(nurseZombie.nurseZombieIdleState);
            return;
        }

        if (IsNearPlayer() && !nurseZombie.IsPlayerLookingAtMe())
        {
            fsm.ChangeState(nurseZombie.nurseZombieAttackState);
            return;
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

        afterLastFootStep += Time.deltaTime;

        if (afterLastFootStep >= nurseZombie.footStepRate)
        {
            afterLastFootStep = 0;
            AudioManager.Instance.Audio3DPlay(nurseZombie.chaseFootStepClip, nurseZombie.transform.position);
        }
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
                nurseZombie.blockedByDoorCount++;

                if (nurseZombie.blockedByDoorCount >= nurseZombie.maxBlockedCount)
                {
                    nurseZombie.gameObject.SetActive(false);
                }

                nurseZombie.MoveToSpawnPosition(new Vector3(-5.96f, 5.5f, -19.71f), Quaternion.identity);
                fsm.ChangeState(nurseZombie.nurseZombieIdleState);
                nurseZombie.haveSeenPlayer = false;
                return;
            }
        }
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);
        return distance <= nurseZombie.attackRange;
    }
}