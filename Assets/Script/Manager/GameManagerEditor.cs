using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        GameManager manager = (GameManager)target;

        Vector3 worldPos = manager.transform.position + manager.SpawnPoint;

        EditorGUI.BeginChangeCheck();
        Vector3 newWorldPos = Handles.PositionHandle(worldPos, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(manager, "Move Spawn Point");
            
            var localPos = newWorldPos - manager.transform.position;

            SerializedObject so = new SerializedObject(manager);
            so.FindProperty("spawnPoint").vector3Value = localPos;
            so.ApplyModifiedProperties();
        }
    }
}
#endif