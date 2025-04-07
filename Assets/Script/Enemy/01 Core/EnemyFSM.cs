using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    private EnemyState currentState;

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();  //  지금 상태 종료
        currentState = newState; 
        currentState?.Enter();  // 새로운 상태로 전환
    }

    public void Update()
    {
        currentState?.Update();
    }
}
