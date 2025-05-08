using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class CopZombie : Enemy
{
    // Components
    [HideInInspector] public NavMeshAgent copzombieAgent;
    public Transform target;
    public Animator copZombieAnim;
    public AudioClip copZombieFootStep;
    public AudioClip copZombieChaseClip;
    public AudioClip copZomicCatchPlayerClip;
    public Transform cameraContainer;
    public CinemachineVirtualCamera copZombieVirtualCamera;
    public PlayableDirector playableDirector;

    // Patrol
    public float patrolRange = 20f;
    public float setPointRate = 10f;

    // Door
    private float detectDoorRate = 0.5f;
    private float afterDetectDoor;
    private float detectDoorRange = 3f;
    [SerializeField] private LayerMask doorLayerMask;

    // FootStep
    public float footStepRate = 1f;
    public float afterLastFootStep;

    // Visibility check
    private bool copHasBeenVisible = false;

    // FSM
    public CopZombiePatrolState copZombiePatrolState;
    public CopZombieChaseState copZombieChaseState;
    public CopZombieAttackState copZombieAttackState;

    protected override void Awake()
    {
        copzombieAgent = GetComponent<NavMeshAgent>();
        copZombieAnim = GetComponentInChildren<Animator>();
        playableDirector = GetComponent<PlayableDirector>();
    }

    protected override void Start()
    {
        base.Start();
        fsm = new EnemyStateMachine();

        copZombiePatrolState = new CopZombiePatrolState(this, fsm);
        copZombieChaseState = new CopZombieChaseState(this, fsm);
        copZombieAttackState = new CopZombieAttackState(this, fsm);

        fsm.ChangeState(copZombiePatrolState);
    }

    protected override void Update()
    {
        base.Update();

        FirstVisible(ref copHasBeenVisible, 3);

        afterDetectDoor += Time.deltaTime;
        if (afterDetectDoor >= detectDoorRate)
        {
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectDoorRange, doorLayerMask))
            {
                LockedDoor door = hit.collider.GetComponent<LockedDoor>();
                if (door != null)
                {
                    door.MonstersOpen();
                }
            }
            afterDetectDoor = 0;
        }
    }

    public void PlayerDown()
    {
        playableDirector.Play();
    }
}
