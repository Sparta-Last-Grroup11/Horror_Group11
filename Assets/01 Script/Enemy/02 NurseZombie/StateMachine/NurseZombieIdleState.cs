public class NurseZombieIdleState : EnemyBaseState  // 기본 상태일 때
{
    private NurseZombie nurseZombie;

    public NurseZombieIdleState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        nurseZombie = enemy as NurseZombie;
    }

    public override void Enter()
    {
        nurseZombie.nurseZombieAnim.SetBool("IsChasing", false);
        GameManager.Instance.player.isChased = false;
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer() && !nurseZombie.IsPlayerLookingAtMe() && !nurseZombie.lightStateSO.IsLightOn)
        {
            fsm.ChangeState(new NurseZombieChaseState(nurseZombie, fsm));
        }
    }

}
