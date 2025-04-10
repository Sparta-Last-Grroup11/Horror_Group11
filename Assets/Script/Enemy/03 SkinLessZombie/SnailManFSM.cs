public class SnailManFSM : E_StateMachine
{
    public SnailManFSM(SkinLessZombie snail)
    {
        // 초기 상태 설정
        ChangeState(new Snail_PatrolState(snail, this));
    }

}
