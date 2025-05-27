using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    [Header("Components")]
    public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Rigidbody rb;
    public Animator nurseZombieAnim;
    public LightStateSO lightStateSO;
    public AudioClip nurseZombieChaseClip;
    public AudioClip nurseZombieCatchPlayerClip;
    public CinemachineVirtualCamera nurseZombieVirtualCamera;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float dashSpeed = 6f;
    public float attackRange;
    public float dashTriggerRange = 0.5f;

    [Header("Detection & States")]
    public float detectionRange = 10f;
    public bool hasBeenSeenByPlayer = false;
    public int firstMonologueNum = 4;
    [HideInInspector] public bool hasDashed = false;

    [Header("Door Detection")]
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    [SerializeField] private LayerMask doorLayerMask;
    public LayerMask DoorLayerMask => doorLayerMask;

    [Header("Chase Reset")]
    public float PlayerDisappearTime = 3.0f;
    [SerializeField] private Transform spawnPoint;

    [Header("FSM")]
    public NurseZombieIdleState nurseZombieIdleState;
    public NurseZombieChaseState nurseZombieChaseState;
    public NurseZombieAttackState nurseZombieAttackState;

    [Header("State Tracking")]
    public int blockedByDoorCount = 0;
    public int maxBlockedCount = 2;

    [Header("Audio")]
    public float footStepRate;
    public AudioClip chaseFootStepClip;

    protected override void Awake()
    {
        base.Awake();
        nurseZombieAnim = GetComponentInChildren<Animator>();
        nurseZombieAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        doorLayerMask = LayerMask.GetMask("Interactable");
    }

    protected override void Start()
    {
        base.Start();

        fsm = new EnemyStateMachine();

        nurseZombieIdleState = new NurseZombieIdleState(this, fsm);
        nurseZombieChaseState = new NurseZombieChaseState(this, fsm);
        nurseZombieAttackState = new NurseZombieAttackState(this, fsm);

        fsm.SetDefaultState(nurseZombieIdleState);
        fsm.ChangeState(nurseZombieIdleState);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        gameObject.SetActive(true);
        Vector3 spawnPos = new Vector3(-5.96f, 5.5f, -19.71f);
        Quaternion spawnRot = Quaternion.identity;
        MoveToSpawnPosition(spawnPos, spawnRot);
        blockedByDoorCount = 1;
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * 1.6f);
        bool isInView = viewPos.z > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;

        if (isInView)
        {
            Vector3 cameraPos = Camera.main.transform.position + Camera.main.transform.forward * 0.1f;
            Vector3 direction = (transform.position + Vector3.up * 1.7f) - cameraPos;
            float distance = direction.magnitude;

            Debug.DrawRay(cameraPos, direction, Color.red, distance);
            if (Physics.Raycast(cameraPos, direction, out RaycastHit hit, distance))
            {
                if (hit.transform == this.transform)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void MoveToSpawnPosition(Vector3 targetPosition, Quaternion targetRotation)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 2f, NavMesh.AllAreas))
        {
            nurseZombieAgent.Warp(hit.position);
            transform.rotation = targetRotation;
        }
        else
        {
            Debug.LogWarning("NavMesh에서 유효한 위치를 찾지 못함");
        }
    }
}
