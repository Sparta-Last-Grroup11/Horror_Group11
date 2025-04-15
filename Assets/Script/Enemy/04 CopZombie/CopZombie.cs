using UnityEngine;
using UnityEngine.AI;

public class CopZombie : Enemy
{
    [HideInInspector] public NavMeshAgent copzombieAgent;

    public Transform target;
    public Animator copZombieAnim;
    public float patrolRange = 20f;

    // Door
    private float detectDoorRate = 0.5f;
    private float afterDetectDoor;
    private float detectDoorRange = 1f;
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
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.red, 1f);

        afterDetectDoor += Time.deltaTime;
        if (afterDetectDoor >= detectDoorRate)
        {
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectDoorRange, doorLayerMask))
            {
                ControlDoor door = hit.collider.GetComponent<ControlDoor>();
                if (door != null)
                {
                    door.OpenTheDoor();
                }
            }
            afterDetectDoor = 0;
        }
    }
}
