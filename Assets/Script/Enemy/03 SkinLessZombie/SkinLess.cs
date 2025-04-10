using UnityEngine;
using UnityEngine.AI;

public class SkinLess : Enemy   // 스네일맨 기믹
{
    public Animator SkinLessAnimator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Vector3 OriginalPosition { get; private set; }

    public Transform[] patrolPoints;  //  좀비가 돌아다니는 길
    public float patrolSpeed = 1.5f;  // 평소 정찰 속도
    public float chaseSpeed = 5f;  // 플레이어 쫓아오는 속도
    public float detectionRange = 5f;  // 감지가 풀리는 거리

    protected override void Awake()
    {
        base.Awake();
        OriginalPosition = transform.position;
        SkinLessAnimator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();

        fsm = new E_StateMachine();
        fsm.ChangeState(new SkinLess_PatrolState(this, fsm, patrolPoints));
    }

    private void Start()
    {
        fsm.Update();
    }

    public void MoveTo(Vector3 pos)
    {
        Agent.SetDestination(pos);  // NavMesh로 pos까지 가도록 설정
        SkinLessAnimator.SetFloat("Speed", Agent.speed);
    }

    public bool IsPlayerFar()
    {
        return Vector3.Distance(Player.position, transform.position) > detectionRange;
    }

}
