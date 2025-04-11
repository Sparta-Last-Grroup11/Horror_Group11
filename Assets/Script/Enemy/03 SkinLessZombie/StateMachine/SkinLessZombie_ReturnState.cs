using UnityEngine;

public class SkinLessZombie_ReturnState : E_BaseState
{
    private SkinLessZombie skinLess;
    private Vector3 origin;

    public SkinLessZombie_ReturnState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        skinLess = enemy as SkinLessZombie;
        origin = skinLess.OriginalPosition;
    }

    public override void Enter()
    {
        skinLess.Agent.speed = skinLess.patrolSpeed;
        skinLess.MoveTo(origin);
    }

    public override void Update()
    {
        if (Vector3.Distance(skinLess.transform.position, origin) < 0.3f)
        {
            int closestIndex = skinLess.GetClosestPatrolPointIndex();
            fsm.ChangeState(new SkinLessZombie_PatrolState(skinLess, fsm, skinLess.patrolPoints, closestIndex));
        }
    }
}
