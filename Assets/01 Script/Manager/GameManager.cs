using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using static NurseZombie;

public class GameManager : Singleton<GameManager>
{
    [Header("No Need to Allocate")]
    public Player player;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] Vector3 checkPoint;
    [SerializeField] private bool isSaved;
    [SerializeField] private int life;

    public Enemy cop;
    public Enemy nurse;
    private SpawnNursePhase nursePhase;
    public void SetNursePhase(SpawnNursePhase phase) => nursePhase = phase;
    public SpawnNursePhase GetNursePhase() => nursePhase;

    protected override bool dontDestroy => false;

    public Vector3 SpawnPoint => spawnPoint;

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
        Debug.Log($"[GameManager] CheckPoint SAVED at: {pos}");
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
            cop.ResetEnemy();
        }
        if (nurse != null)
        {
            if (nurse is NurseZombie nurseZombie)
            {
                nurseZombie.ResetEnemy(GetNursePhase());
            }
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
