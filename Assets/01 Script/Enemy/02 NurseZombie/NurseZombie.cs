using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    // Components
    public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Animator nurseZombieAnim;
    [HideInInspector] public Rigidbody rb;

    // Chase, Attack
    public float moveSpeed = 2;
    public float attackRange = 2f;
    public float detectionRange = 10f;
    public float dashSpeed = 6f; // 돌진 속도, 일반 추격보다 빠르게

    // Door
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    public bool hasDetectedPlayer = false;
    [SerializeField] private LayerMask doorLayerMask;

    // Sound
    public AudioClip nurseZombieChaseClip;

    public LayerMask DoorLayerMask
    {
        get { return doorLayerMask; }
    }

    private void Awake()
    {
        nurseZombieAnim = GetComponentInChildren<Animator>();
        nurseZombieAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    protected override void Start()
    {
        base.Start();

        doorLayerMask = LayerMask.GetMask("Interactable");

        Collider nurseCollider = GetComponent<Collider>();
        Collider playerCollider = PlayerTransform.GetComponent<Collider>();
        if (nurseCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(nurseCollider, playerCollider);
        }

        InitNurseFSM();
    }

    private void InitNurseFSM()
    {
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
        // 이후에 여기에 스폰위치를 가져올 예정.
    }
}
