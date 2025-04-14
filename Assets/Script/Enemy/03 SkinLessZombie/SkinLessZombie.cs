using UnityEngine;
using UnityEngine.AI;

public class SkinLessZombie : Enemy   // 스네일맨 기믹
{
    public Animator SkinLessAnimator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Vector3 OriginalPosition { get; private set; }

    public Transform[] patrolPoints;  //  좀비가 돌아다니는 길
    public float patrolSpeed = 1.5f;  // 평소 정찰 속도
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
        int startIndex = GetClosestPatrolPointIndex();
        fsm.ChangeState(new SkinLessZombie_PatrolState(this, fsm, patrolPoints, startIndex));
    }

    public void MoveTo(Vector3 pos)
    {
        Agent.SetDestination(pos);  // NavMesh로 pos까지 가도록 설정
    }

    public int GetClosestPatrolPointIndex()  // 가장 가까운 지점으로 돌아가 순찰하도록 설정
    {
        int closestIndex = 0;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

}
