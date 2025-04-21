using UnityEngine;

// Overlap방식을 통한 레이캐스트 탐지 가능한지 테스트 중입니다. 

public class EnemyVisionSystem : MonoBehaviour
{
    [Header("시야 설정")]
    public float viewAngle = 360f;
    public float viewDistance = 10f;

    [Header("레이어 설정")]
    [SerializeField] private LayerMask notEnemyLayer;
    public LayerMask playerLayer;

    public bool CanSeePlayerNewVer(Transform enemyTransform, out Transform seenPlayer)  // 기존 메서드에서는 간호사가 
    {
        seenPlayer = null;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(enemyTransform.position, viewDistance, playerLayer);

        foreach (var target in targetsInViewRadius)
        {
            if (!Physics.Linecast(enemyTransform.position + Vector3.up, target.transform.position + Vector3.up, notEnemyLayer))
            {
                seenPlayer = target.transform;
                return true;
            }
        }

        return false;
    }
}
