using UnityEngine;

public class SkinLessFSM : E_StateMachine
{
    public SkinLessFSM(SkinLess skinLess, Transform[] patrolPoints)
    {
        // 초기 상태 설정
        ChangeState(new SkinLess_PatrolState(skinLess, this, patrolPoints));
    }

}
