using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

//게임 플로우메이커 커스텀 에디터(각 오브젝트에 핸들 붙이기) => 좌표값으로 매번 위치 수정하기 번거로워 핸들을 붙이기 위함.
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