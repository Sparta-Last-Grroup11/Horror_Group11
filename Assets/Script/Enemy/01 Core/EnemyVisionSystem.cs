using UnityEngine;

// Overlap방식을 통한 레이캐스트 탐지 가능한지 테스트 중입니다. 

public class EnemyVisionSystem : MonoBehaviour
{
    [Header("시야 설정")]
    public float viewAngle = 90f;
    public float viewDistance = 10f;

    [Header("레이어 설정")]
    [SerializeField] private LayerMask notEnemyLayer;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    [Header("디버그 옵션")]
    public bool drawDebug = true;

    public bool EnemyCanSeePlayer(Transform enemyTransform, out Transform seenPlayer)
    {
        seenPlayer = null;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(enemyTransform.position, viewDistance, playerLayer);

        foreach (var target in targetsInViewRadius)
        {
            Vector3 dirToTarget = (target.transform.position - enemyTransform.position).normalized;
            float angle = Vector3.Angle(enemyTransform.forward, dirToTarget);

            if (angle < viewAngle / 2f)
            {
                // 시야 안에 있음
                if (!Physics.Linecast(enemyTransform.position + Vector3.up, target.transform.position + Vector3.up, notEnemyLayer))
                {
                    seenPlayer = target.transform;
                    return true;
                }
                else
                {
                    if (drawDebug)
                        Debug.DrawLine(enemyTransform.position + Vector3.up, target.transform.position + Vector3.up, Color.yellow); // 막힘
                }
            }

            if (drawDebug)
                Debug.DrawLine(enemyTransform.position + Vector3.up, target.transform.position + Vector3.up, Color.red); // 각도밖
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 rightLimit = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;
        Vector3 leftLimit = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + rightLimit * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + leftLimit * viewDistance);
    }
}
