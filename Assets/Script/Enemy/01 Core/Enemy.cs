using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected E_StateMachine fsm;

    [SerializeField] protected Transform Player;
    [SerializeField] private float viewAngle = 90f;  

    protected virtual void Awake()
    {
        fsm = new E_StateMachine();
    }

    protected virtual void Update()
    {
        fsm?.Update();
    }

    public bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (Player.position - transform.position).normalized;  //  몬스터에서 플레이어로 가는 방향 벡터
        float angle = Vector3.Angle(transform.forward, dirToPlayer);  // 내가 보고 있는 방향과 플레이어 방향의 각도를 계산 

        if (angle < viewAngle / 2f)  // 왼쪽 오른쪽 각도 나누고
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dirToPlayer, out hit))   
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public virtual void StartAction()  // 몬스터가 추격 시작할 때 호출
    {
        // animator.SetTrigger("Run");
    }

    public virtual void Attack()  // 몬스터가 공격할 때 호출
    {
        // animator.SetTrigger("Attack");
    }
}
