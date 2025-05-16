using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateMachine fsm;
    protected Transform playerTransform;

    public Transform PlayerTransform => playerTransform;  // 외부 접근용 getter
    [SerializeField] private LayerMask notEnemyLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] protected LayerMask doorLayer;
    [HideInInspector] public bool isDoorOpened = false;
    [SerializeField] protected float doorDetectRange = 2f;

    private float afterPlayerDisappear;
    private float detectPlayerRate = 5f;
    public bool haveSeenPlayer = false; //플레이어를 한 번이라도 본 적이 있는지

    public float viewDistance = 10f;
    public float viewAngle = 90f;

    protected virtual void Awake()
    {
        doorLayer = LayerMask.GetMask("Interactable");
    }

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

    public bool HasLostPlayer()
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

    public void LookAtPlayer() 
    {
        Vector3 dir = PlayerTransform.position - transform.position;
        dir.y = 0;  // y축 회전 제거
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void FirstVisible(ref bool hasBeenVisible, int monologueNum)
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
}
