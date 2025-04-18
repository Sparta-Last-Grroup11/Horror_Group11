using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Flow
{
    public string key;
    public Color color;
    public Color textColor;
    [Range(0,40)]
    public int fontSize;
    public List<Vector3> value;
}
public class GameFlowMaker : MonoBehaviour
{
    [SerializeField] List<Flow> unitFlow;
    public List<Flow> UnitFlow => unitFlow;

    public void Awake()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        foreach (Flow flow in unitFlow)
        {
            Gizmos.color = flow.color;

            GUIStyle labelStyle = new GUIStyle();
            labelStyle.normal.textColor = flow.textColor;
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.fontSize = flow.fontSize;

            int num = 1;
            foreach (Vector3 v in flow.value)
            {
                Handles.Label(v + Vector3.up * 1f, flow.key + " " + num.ToString(), labelStyle);
                Gizmos.DrawCube(transform.position + v, Vector3.one * 0.5f);
                num++;
            }

            for (int i = 0; i < flow.value.Count - 1; i++)
                Gizmos.DrawLine(flow.value[i], flow.value[i + 1]);
        }
#endif
    }
}
