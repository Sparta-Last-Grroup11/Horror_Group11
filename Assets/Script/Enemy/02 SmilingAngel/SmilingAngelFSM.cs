using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilingAngelFSM : E_StateMachine
{
    public SmilingAngelFSM(SmilingAngel angel)
    {
        // 초기 상태 설정
        ChangeState(new Angel_IdleState(angel, this));
    }

}
