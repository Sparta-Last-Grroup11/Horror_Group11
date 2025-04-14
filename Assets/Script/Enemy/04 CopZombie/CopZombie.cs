using UnityEngine;
using UnityEngine.AI;

public class CopZombie : Enemy
{
    [HideInInspector] public NavMeshAgent copzombieAgent;

    public Transform target;
    public Animator copZombieAnim;
    public float patrolRange = 20f;

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
}
