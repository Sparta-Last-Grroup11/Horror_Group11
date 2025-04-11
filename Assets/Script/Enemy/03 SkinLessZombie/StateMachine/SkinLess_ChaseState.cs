using UnityEngine;

public class SkinLess_ChaseState : E_BaseState
{
    private SkinLess skinLess;

    public SkinLess_ChaseState(Enemy enemy, E_StateMachine fsm) : base(enemy, fsm)
    {
        skinLess = enemy as SkinLess;
    }

    public override void Enter()
    {
        skinLess.Agent.speed = skinLess.chaseSpeed;
        skinLess.SkinLessAnimator.speed = 1.5f;

        skinLess.Agent.isStopped = false;
        skinLess.Agent.SetDestination(skinLess.PlayerTransform.position);
    }

    public override void Update()
    {
        skinLess.Agent.SetDestination(skinLess.PlayerTransform.position);

        if (!skinLess.IsPlayerFar())
        {
            fsm.ChangeState(new SkinLess_ReturnState(skinLess, fsm));
        }
    }
}
