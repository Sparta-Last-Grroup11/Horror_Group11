using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    [Header("Components")]
    [HideInInspector] public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Rigidbody rb;
    public Animator nurseZombieAnim;
    public AudioClip nurseZombieChaseClip;
    public LightStateSO lightStateSO;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float dashSpeed = 6f;
    public float attackRange = 2f;
    public float dashTriggerRange = 0.5f;

    [Header("Detection & States")]
    public float detectionRange = 10f;
    public bool hasDetectedPlayer = false;
    public bool hasBeenSeenByPlayer = false;
    public int firstMonologueNum = 4;
    public bool hasDashed = false;

    [Header("Door Detection")]
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    [SerializeField] private LayerMask doorLayerMask;
    public LayerMask DoorLayerMask => doorLayerMask;

    [Header("Chase Reset")]
    public float PlayerDisappearTime = 3.0f;
    public float waitTimer = 0f;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        nurseZombieAnim = GetComponentInChildren<Animator>();
        nurseZombieAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        doorLayerMask = LayerMask.GetMask("Interactable");
    }

    protected override void Start()
    {
        base.Start();
        Collider nurseCollider = GetComponent<Collider>();
        Collider playerCollider = PlayerTransform.GetComponent<Collider>();
        if (nurseCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(nurseCollider, playerCollider);
        }

        fsm = new EnemyStateMachine();
        fsm.ChangeState(new NurseZombieIdleState(this, fsm));
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - PlayerTransform.position).normalized;  // 플레이어에서 몬스터를 향하는 방향 벡터
        Vector3 playerforward = PlayerTransform.forward.normalized;  // 플레이어가 보고 있는 방향 벡터

        float dot = Vector3.Dot(toNurse, playerforward);  // 1에 가까울수록 플레이어 = 몬스터 같은 방향
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
    }

    public void MoveToSpawnPosition()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("NurseZombie's SpawnPoint가 설정되지 않음");
            return;
        }

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoint.position, out hit, 2f, NavMesh.AllAreas))
        {
            nurseZombieAgent.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning("NavMesh에서 유효한 위치를 찾지 못함");
        }
    }
}
