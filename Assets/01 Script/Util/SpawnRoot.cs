using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SpawnRoot : MonoBehaviour
{
    public Dictionary<string, List<Transform>> spawnPoints = new Dictionary<string, List<Transform>>();

    [System.Serializable]
    public class NamedSpawnList
    {
        public string name;
        public List<Transform> points;
    }

    [SerializeField]
    private List<NamedSpawnList> debugView = new();

    private void Awake()
    {
        BuildSpawnPoints();
    }

    public void Start()
    {
        StageManager.Instance.spawnRoot = this;
    }

    [ContextMenu("Build Spawn Points Dictionary")]
    private void BuildSpawnPoints()
    {
        spawnPoints.Clear();
        debugView.Clear();

        foreach (Transform group in transform)
        {
            string groupName = group.name;
            List<Transform> childPoints = new List<Transform>();

            foreach (Transform point in group)
            {
                childPoints.Add(point);
            }

            spawnPoints.Add(groupName, childPoints);

            debugView.Add(new NamedSpawnList
            {
                name = groupName,
                points = new List<Transform>(childPoints)
            });
        }

        Debug.Log("✅ Spawn points dictionary built.");
    }
}
