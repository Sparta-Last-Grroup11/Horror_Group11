using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("No Need to Allocate")]
    public Player player;
    [SerializeField] Vector3 spawnPoint;
    private NavMeshSurface surface;
    protected override bool dontDestroy => false;

    public Vector3 SpawnPoint => spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        surface = Instantiate(ResourceManager.Instance.Load<GameObject>(ResourceType.Enemy, "NavMeshSurface_Hospital")).GetComponent<NavMeshSurface>();
        SpawnCharacter();
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

    public void SurfaceUpdate()
    {
        surface.BuildNavMesh();
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
