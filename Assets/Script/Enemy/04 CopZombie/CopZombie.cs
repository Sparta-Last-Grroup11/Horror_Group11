using UnityEngine;
using UnityEngine.AI;

public class CopZombie : Enemy
{
    [HideInInspector] public NavMeshAgent copzombieAgent;

    public Transform target;
    public Animator copZombieAnim;
    public float patrolRange = 20f;

    public float setPointRate = 10f;

    // Door
    private float detectDoorRate = 0.5f;
    private float afterDetectDoor;
    private float detectDoorRange = 2f;
    [SerializeField] private LayerMask doorLayerMask;

    private void Awake()
    {
        copzombieAgent = GetComponent<NavMeshAgent>();
        copZombieAnim = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        fsm = new E_StateMachine();
        fsm.ChangeState(new CopZombie_PatrolState(this, fsm));
    }

    protected override void Update()
    {
        base.Update();
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.red, 2f);

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
}
