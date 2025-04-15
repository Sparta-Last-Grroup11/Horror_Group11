using UnityEngine;
using UnityEngine.AI;

public class SkinLessZombie : Enemy   // 점프스케어 (플레이어 보면 빠르게 달려와서 깜놀시키고, 사라짐, 무해함)
{
    public Animator SkinLessAnimator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Vector3 OriginalPosition { get; private set; }

    public float chaseSpeed = 80f;  // 플레이어 쫓아오는 속도
    public float detectionRange = 5f;  // 감지가 풀리는 거리

    protected override void Start()
    {
        base.Start();
        OriginalPosition = transform.position;
        SkinLessAnimator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();

        InitSkinLessFSM();
        fsm.Update();
    }

    private void InitSkinLessFSM()
    {
        fsm = new E_StateMachine();
        fsm.ChangeState(new SkinLessZombie_AmbushState(this, fsm));
    }

}
