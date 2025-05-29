using Cinemachine;
using System.Collections;
using UnityEngine;

//게임 매니저 역할인데 게임 시작 여부, 시작 시에 캐릭터의 스폰, 게임 진행 상황 저장 그리고 Json으로 스폰시키지 않는 친구의 스폰을 담당.
public class GameManager : Singleton<GameManager>
{
    [Header("No Need to Allocate")]
    public Player player;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] Vector3 checkPoint;
    [SerializeField] private bool isSaved;
    [SerializeField] private int life;
    public int Life => life;
    public CopZombieCinematicTrigger copCinematicTrigger;
    public Enemy nurse;
    public Enemy cop;
    protected override bool dontDestroy => false;

    public Vector3 SpawnPoint => spawnPoint;

    private NurseZombie.SpawnNursePhase currentPhase;

    protected override void Awake()
    {
        base.Awake();
        SpawnCharacter();
        isSaved = false;
        life = 3;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            CheckPointLoad();
    }

    public void CheckPointSave(Vector3 pos)
    {
        checkPoint = pos;
        isSaved = true;
    }

    public void CheckPointLoad()
    {
        if (isSaved && player != null)
        {
            player.cantMove = false;
            player.isDead = false;
            player.transform.position = checkPoint;
        }
        else
        {
            player.cantMove = false;
            player.isDead = false;
            player.transform.position = spawnPoint;
        }
        if (cop != null)
        {
            Destroy(cop.gameObject);
            StartCoroutine(SetTriggerCopCinematic());
        }
        if (nurse != null && nurse is NurseZombie nurseZombie)
        {
            nurseZombie.ResetEnemy(currentPhase);
        }
        life -= 1;
    }

    void SpawnCharacter()
    {
        if (player != null)
            return;
        GameObject VirtualCam = Instantiate(Resources.Load<GameObject>("Player/Virtual Camera"));
        GameObject playerPrefab = ResourceManager.Instance.Load<GameObject>(ResourceType.Player, "Player");
        player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity).GetComponent<Player>();
        player.virtualCamera = VirtualCam.GetComponent<CinemachineVirtualCamera>();
        VirtualCam.GetComponent<CinemachineVirtualCamera>().Follow = player.cameraContainer;
    }

    public void SetNursePhase(NurseZombie.SpawnNursePhase phase)
    {
        currentPhase = phase;
    }

    private IEnumerator SetTriggerCopCinematic()
    {
        yield return new WaitForSeconds(1f);
        copCinematicTrigger.boxCollider.enabled = true;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(spawnPoint, Vector3.one);
        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.Label(spawnPoint + Vector3.up * 1f, "Spawn Point");
#endif
    }
}
