using UnityEngine;

public class SkinLess_ReturnState : E_BaseState
{
    private SkinLess skinLess;
    private Vector3 origin;

    public SkinLess_ReturnState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        skinLess = enemy as SkinLess;
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
            fsm.ChangeState(new SkinLess_PatrolState(skinLess, fsm, skinLess.patrolPoints));
        }
    }
}
