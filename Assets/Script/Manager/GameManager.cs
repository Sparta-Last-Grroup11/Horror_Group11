using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("No Need to Allocate")]
    public Player player;
    [SerializeField] Vector3 spawnPoint;
    public Camera subCam;

    public Vector3 SpawnPoint => spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        SpawnCharacter();
        subCam = GameObject.Find("Sub Camera").GetComponent<Camera>();
    }

    void SpawnCharacter()
    {
        GameObject VirtualCam = Instantiate(Resources.Load<GameObject>("Player/Virtual Camera"));
        player = Instantiate(Resources.Load<GameObject>("Player/Player"), spawnPoint, Quaternion.identity).GetComponent<Player>();
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
