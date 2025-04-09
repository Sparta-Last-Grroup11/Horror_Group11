public class NurseFSM : E_StateMachine
{
    public NurseFSM(Nurse nurse)
    {
        // 초기 상태 설정
        ChangeState(new Nurse_IdleState(nurse, this));
    }

}
