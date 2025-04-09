public class Angel_IdleState : E_BaseState  // 기본 상태일 때  (애니메이션 제어용인지 아니면 기능포함해서 위치변환) 
{
    private SmilingAngel angel;

    public Angel_IdleState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        angel = enemy as SmilingAngel;
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer() && !angel.IsPlayerLookingAtMe())  // 1) 플레이어가 처음 시야각에 들어오고, 2) 플레이어가 날 보지 않을 때 
        {
            fsm.ChangeState(new Angel_ChaseState(angel, fsm));  // 추적Chase 상태로 전환
        }
    }
}
