using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameFlowMaker))]
public class GameFlowMakerEditor : Editor
{
    private void OnSceneGUI()
    {
        GameFlowMaker gameFlow = (GameFlowMaker)target;

        for (int a = 0; a < gameFlow.UnitFlow.Count; a++)
        {
            Flow f = gameFlow.UnitFlow[a];
            for (int b = 0; b < f.value.Count; b++)
            {
                Vector3 worldPos = gameFlow.transform.position + f.value[b].Vec;
                EditorGUI.BeginChangeCheck();
                Vector3 newWorldPos = Handles.PositionHandle(worldPos, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(gameFlow, "Move Flow Point");

                    f.value[b].Vec = newWorldPos - gameFlow.transform.position;

                    EditorUtility.SetDirty(gameFlow);
                }
            }
        }
    }
}
#endif