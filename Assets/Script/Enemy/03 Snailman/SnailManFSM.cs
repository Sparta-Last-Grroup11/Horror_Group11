using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailManFSM : E_StateMachine
{
    public SnailManFSM(SnailMan snail)
    {
        // 초기 상태 설정
        ChangeState(new Snail_IdleState(snail, this));
    }

}
