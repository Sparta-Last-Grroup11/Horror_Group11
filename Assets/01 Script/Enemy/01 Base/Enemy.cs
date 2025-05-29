using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateMachine fsm;
    protected Transform playerTransform;

    public Transform PlayerTransform => playerTransform;
    [SerializeField] private LayerMask notEnemyLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] protected LayerMask doorLayer;
    [HideInInspector] public bool isDoorOpened = false;
    [SerializeField] protected float doorDetectRange = 2f;

    private float afterPlayerDisappear;
    private float detectPlayerRate = 5f;
    public bool haveSeenPlayer = false;

    public float viewDistance = 10f;
    public float viewAngle = 90f;

    protected virtual void Awake()
    {
        doorLayer = LayerMask.GetMask("Interactable");
    }

    protected virtual void Start()
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

    public bool CanSeePlayer()  // 플레이어가 시야각 내에 있고 RayCast에 막히지 않았는지 확인
    {
        if (playerTransform == null) return false;
        
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float angle = Vector3.Angle(transform.forward, dirToPlayer); 

        if (angle < viewAngle / 2f)
        {
            Debug.DrawRay(transform.position, dirToPlayer * distanceToPlayer, Color.green);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dirToPlayer, out hit, distanceToPlayer, notEnemyLayer))   
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    haveSeenPlayer = true;
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

    public bool HasLostPlayer() // 일정 시간 동안 플레이어를 보지 못했는지 확인
    {
        if (CanSeePlayer())
        {
            afterPlayerDisappear = 0;
            return false;
        }
        else
        {
            afterPlayerDisappear += Time.deltaTime;
            return afterPlayerDisappear > detectPlayerRate;
        }
    }

    public void LookAtPlayer() // y축을 제외한 방향으로 플레이어를 바라봄
    {
        Vector3 dir = PlayerTransform.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void FirstVisible(ref bool hasBeenVisible, int monologueNum) // 플레이어에게 처음 보였을 때 1회성 모놀로그와 효과 재생
    {
        if (hasBeenVisible == false)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            bool isInView = viewPos.z > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;

            if (isInView)
            {
                Vector3 cameraPos = Camera.main.transform.position;
                Vector3 direction = transform.position - cameraPos;
                float distance = direction.magnitude;

                if (Physics.Raycast(cameraPos, direction, out RaycastHit hit, distance))
                {
                    if (hit.transform == this.transform)
                    {
                        MonologueManager.Instance.DialogPlay(monologueNum);
                        AudioManager.Instance.Audio2DPlay(GameManager.Instance.player.shockedClip, 1, false, EAudioType.SFX);
                        hasBeenVisible = !hasBeenVisible;
                    }
                }
            }
        }
        else return;
    }

    public virtual void ResetEnemy() { }
}
