using UnityEngine;
using UnityEngine.AI;

public class CopZombie : Enemy
{
    [HideInInspector] public NavMeshAgent copzombieAgent;

    public Transform target;
    public Animator copZombieAnim;
    public float patrolRange = 20f;

    protected override void Start()
    {
        base.Start();
        copzombieAgent = GetComponent<NavMeshAgent>();
        copZombieAnim = GetComponentInChildren<Animator>();
        fsm.ChangeState(new CopZombie_PatrolState(this, fsm));
    }
}
