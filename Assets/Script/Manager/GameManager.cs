using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Camera subCam;
    [SerializeField] Vector3 spawnPoint;

    public Vector3 SpawnPoint => spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        subCam = GameObject.Find("Sub Camera").GetComponent<Camera>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
