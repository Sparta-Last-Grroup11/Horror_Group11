using UnityEngine;

public class NurseZombie_ChaseState : EnemyBaseState    // 플레이어를 추격하는 상태일 때
{
    public NurseZombie nurseZombie;

    // Reset
    private float PlayerDisappearTime = 3.0f;
    private float waitTimer = 0f;

    // Glitch
    private GlitchUI glitchUI;
    private bool isGlitchOn = false;

    // HeartBeat
    private HeartBeat heartBeat;
    private AudioClip heartBeatClip;
    AudioSource heartBeatSource;

    public NurseZombie_ChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", true);
        GameManager.Instance.player.isChased = true;
        waitTimer = 0f;

        // GlitchUI
        glitchUI = UIManager.Instance.show<GlitchUI>();
        isGlitchOn = false;

        // HeartBeatUI
        heartBeat = UIManager.Instance.Get<HeartBeat>();
        if (heartBeat == null)
        {
            heartBeat = UIManager.Instance.show<HeartBeat>();
        }
        else
        {
            heartBeat.gameObject.SetActive(true);
        }

        heartBeat.ChanbeatSpeed(1f);
        heartBeatClip = Resources.Load<AudioClip>("Sound/HeartbeatSound");
        heartBeatSource = nurseZombie.GetComponent<AudioSource>();
        heartBeatSource = nurseZombie.gameObject.AddComponent<AudioSource>();
    }

    public override void Update()
    {
        if (nurseZombie.PlayerTransform == null) return;

        CheckIfPlayerInRoom();
        HandleGlitchEffectAndHeartBeat();
        TransitionToAttack();

        nurseZombie.MoveTowardsPlayer(nurseZombie.moveSpeed);  // 플레이어를 뒤쫓아 움직임
    }

    public void CheckIfPlayerInRoom()
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
                    PlayerInRoom();
                    return;
                }
            }
            nurseZombie.afterDetectDoor = 0;
        }
    }

    public void PlayerInRoom()
    {
        waitTimer += Time.deltaTime;
        heartBeat.ChanbeatSpeed(0f);
        if (waitTimer >= PlayerDisappearTime)  // 방 밖에서 일정 시간 대기 후 스폰 위치로 이동, 다시 IdleState로 전환
        {
            nurseZombie.MoveToSpawnPosition();
            fsm.ChangeState(new NurseZombie_IdleState(nurseZombie, fsm));
        }
        return;
    }

    public void HandleGlitchEffectAndHeartBeat()
    {
        if (nurseZombie.IsPlayerLookingAtMe())
        {
            if (!isGlitchOn && glitchUI != null)
            {
                glitchUI.GlitchStart(50f);
                isGlitchOn = true;
            }

            heartBeat.ChanbeatSpeed(0f);

            if (heartBeatSource != null && heartBeatSource.isPlaying)
                heartBeatSource.Stop();

            fsm.ChangeState(new NurseZombie_IdleState(nurseZombie, fsm));
            return;

        }
        else
        {
            if (isGlitchOn && glitchUI != null)
            {
                glitchUI.GlitchEnd();
                isGlitchOn = false;

            }

            heartBeat.ChanbeatSpeed(1f);

            if (!heartBeatSource.isPlaying)
            {
                heartBeatSource.Play(); // 다시 재생
            }

        }
    }

    public void TransitionToAttack()
    {
        if (IsNearPlayer())  // 천사가 일정 거리 안에 있다면 Attack 상태로 전환
        {
            if (isGlitchOn && glitchUI != null)
            {
                glitchUI.GlitchEnd();
                isGlitchOn = false;
            }

            fsm.ChangeState(new NurseZombie_AttackState(nurseZombie, fsm));
        }
    }

    public bool IsNearPlayer()
    {
        float distance = Vector3.Distance(nurseZombie.transform.position, nurseZombie.PlayerTransform.position);  // 몬스터와 플레이어의 거리
        return distance <= nurseZombie.attackRange;  // 공격 범위 안에 들어왔는지 확인
    }
}