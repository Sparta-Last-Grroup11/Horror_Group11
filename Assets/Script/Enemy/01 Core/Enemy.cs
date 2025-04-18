using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateMachine fsm;
    protected Transform playerTransform;
    public Transform PlayerTransform => playerTransform;  // 외부 접근용 getter
    [SerializeField] private LayerMask notEnemyLayer;

    public float viewAngle = 90f;  

    protected virtual void Start()
    {
        InitPlayerTransform();
    }

    private void InitPlayerTransform()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null)
        {
            Debug.Log("[Enemy] GameManager나 player가 null입니다.");
            playerTransform = null;
        }
        else
        {
            playerTransform = GameManager.Instance.player.transform;
        }
    }

    protected virtual void Update()
    {
        fsm?.Update();
    }

    public bool CanSeePlayer()
    {
        if (playerTransform == null) return false;
        
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;  //  몬스터에서 플레이어로 가는 방향 벡터
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float angle = Vector3.Angle(transform.forward, dirToPlayer);  // 내가 보고 있는 방향과 플레이어 방향의 각도를 계산 

        if (angle < viewAngle / 2f)  // 왼쪽 오른쪽 시야각 안에 있는지 판별
        {
            Debug.DrawRay(transform.position, dirToPlayer * distanceToPlayer, Color.green);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dirToPlayer, out hit, distanceToPlayer, notEnemyLayer))   
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    return true;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * distanceToPlayer, Color.red);
        }

        return false;
    }

    public void LookAtPlayer()  // 플레이어를 향해 몸을 돌리게 해줌
    {
        Vector3 dir = PlayerTransform.position - transform.position;
        dir.y = 0;  // y축 회전 제거
        transform.rotation = Quaternion.LookRotation(dir);
    }

}
