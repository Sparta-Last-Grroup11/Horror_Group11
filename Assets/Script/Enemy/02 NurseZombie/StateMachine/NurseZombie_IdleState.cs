public class NurseZombie_IdleState : EnemyBaseState  // 기본 상태일 때
{
    private NurseZombie nurseZombie;

    public NurseZombie_IdleState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
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
        bool canSee = enemy.CanSeePlayer();
        bool isPlayerLooking = nurseZombie.IsPlayerLookingAtMe();

        if (canSee && !isPlayerLooking)
        {
            fsm.ChangeState(new NurseZombie_ChaseState(nurseZombie, fsm));
        }
    }
}
