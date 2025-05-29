using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
//게임 플로우 메이커 만들기 전에 핸들 어떻게 붙이는지 테스트해볼려고 스폰 포인트를 시각화하면서 핸들로 움직일 수 있게 함.

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