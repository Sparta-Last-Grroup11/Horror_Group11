using UnityEngine;
using UnityEngine.AI;

public class SkinLessZombie : Enemy   // 점프스케어 (플레이어 보면 빠르게 달려와서 깜놀시키고, 사라짐, 무해함)
{
    public Animator skinLessZombieAnim { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Vector3 OriginalPosition { get; private set; }

    protected override void Start()
    {
        base.Start();
        OriginalPosition = transform.position;
        skinLessZombieAnim = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.enabled = false;

        InitSkinLessFSM();
        fsm.Update();
    }

    private void InitSkinLessFSM()
    {
        fsm = new E_StateMachine();
        fsm.ChangeState(new SkinLessZombie_AmbushState(this, fsm));
    }

    public void MoveTo(Vector3 destination)
    {
        Agent.SetDestination(destination);
    }

}
