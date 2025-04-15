using UnityEngine;

public class SkinLessZombie_ReturnState : E_BaseState
{
    private SkinLessZombie skinLessZombie;
    private Vector3 origin;

    public SkinLessZombie_ReturnState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        skinLessZombie = enemy as SkinLessZombie;
        origin = skinLessZombie.OriginalPosition;
    }

    public override void Enter()
    {
        skinLessZombie.Agent.speed = skinLessZombie.patrolSpeed;
        skinLessZombie.MoveTo(origin);
    }

    public override void Update()
    {
        if (Vector3.Distance(skinLessZombie.transform.position, origin) < 0.3f)
        {
            int closestIndex = skinLessZombie.GetClosestPatrolPointIndex();
            fsm.ChangeState(new SkinLessZombie_PatrolState(skinLessZombie, fsm, skinLessZombie.patrolPoints, closestIndex));
        }
    }
}
