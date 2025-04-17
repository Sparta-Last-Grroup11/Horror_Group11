using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    // Components
    [HideInInspector] public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Animator nurseZombieAnim;
    [HideInInspector] public Rigidbody rb;

    // Chase, Attack
    public float moveSpeed = 4f;
    public float attackRange = 2f;
    public float detectionRange = 10f;

    // Door
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    [SerializeField] private LayerMask doorLayerMask;

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
        fsm.ChangeState(new NurseZombie_IdleState(this, fsm));
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - PlayerTransform.position).normalized;  // 플레이어에서 몬스터를 향하는 방향 벡터
        Vector3 playerforward = PlayerTransform.forward.normalized;  // 플레이어가 보고 있는 방향 벡터

        float dot = Vector3.Dot(toNurse, playerforward);  // 1에 가까울수록 플레이어 = 몬스터 같은 방향
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
    }

    public void MoveTowardsPlayer(float speed)
    {
        Vector3 direction = (PlayerTransform.position - transform.position).normalized;
        direction.y = 0;
        float distance = Vector3.Distance(transform.position, PlayerTransform.position);
        float minDistance = 1.0f;  //  플레이어와 최소 거리 유지
        if (distance > minDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void MoveToSpawnPosition()
    {
        // 여기에 스폰위치를 가져와주면 될듯.
    }
}
