using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Vector3AndRotation
{
    public Vector3 Vec;
    public Quaternion Rot;
}

[System.Serializable]
public class Flow
{
    public string key;
    public Color color;
    public Color textColor;
    [Range(0,40)]
    public int fontSize;
    public List<Vector3AndRotation> value;
}
public class GameFlowMaker : MonoBehaviour
{
    [SerializeField] private float boxSize = 0.5f;
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
            foreach (var v in flow.value)
            {
                if (num != 1)
                    Handles.Label(v.Vec + Vector3.up * 1f, flow.key + " " + num.ToString(), labelStyle);
                else
                    Handles.Label(v.Vec + Vector3.up * 1f, flow.key, labelStyle);
                Gizmos.DrawCube(transform.position + v.Vec, Vector3.one * boxSize);
                num++;
            }

            for (int i = 0; i < flow.value.Count - 1; i++)
                Gizmos.DrawLine(flow.value[i].Vec, flow.value[i + 1].Vec);
        }
#endif
    }
}
