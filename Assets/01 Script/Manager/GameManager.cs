using Cinemachine;
using UnityEngine;

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
            copCinematicTrigger.boxCollider.enabled = true;
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
