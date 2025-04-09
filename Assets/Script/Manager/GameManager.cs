using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Camera subCam;
    [SerializeField] Vector3 spawnPoint;

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
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(spawnPoint, Vector3.one);
    }
}
