using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected E_StateMachine fsm;

    protected Transform playerTransform;
    public Transform PlayerTransform => playerTransform;  // 외부 접근용 getter
    public LayerMask playerLayer;
    [SerializeField] private float viewAngle = 90f;  

    protected virtual void Awake()
    {
        fsm = new E_StateMachine();
        playerTransform = GameManager.Instance.player.transform;

        if (playerLayer == 0)
            playerLayer = LayerMask.GetMask("Player");
    }

    protected virtual void Update()
    {
        fsm?.Update();
    }

    public bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;  //  몬스터에서 플레이어로 가는 방향 벡터
        float angle = Vector3.Angle(transform.forward, dirToPlayer);  // 내가 보고 있는 방향과 플레이어 방향의 각도를 계산 

        if (angle < viewAngle / 2f)  // 왼쪽 오른쪽 각도 나누고
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dirToPlayer, out hit, Mathf.Infinity, playerLayer))   
            {
                return true;
            }
        }
        return false;
    }

}
