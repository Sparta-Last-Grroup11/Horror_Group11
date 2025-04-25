public class CopZombie_AttackState : EnemyBaseState
{
    private CopZombie copZombie;

    public CopZombie_AttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        copZombie = enemy as CopZombie;
    }

    public override void Enter()
    {
        GameManager.Instance.player.isCaught = true;
        copZombie.copZombieVirtualCamera.Priority = 12;
        copZombie.copZombieAnim.SetTrigger("DoAttack");
        AudioManager.Instance.Audio2DPlay(copZombie.copZomicCatchPlayerClip, 1f);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}
