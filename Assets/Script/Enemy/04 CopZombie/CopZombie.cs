using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class CopZombie : Enemy
{
    E_StateMachine curEnemyState;

    private void Start()
    {
        
    }

    public void ChangeState(PlayerState newState)
    {
        curEnemyState?.Exit();
        curEnemyState = newState;
        curEnemyState.Enter();
    }
}
