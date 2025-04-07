using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSmiler : Enemy
{
    private EnemyState idleState;
    private EnemyState chaseState;
    private EnemyState attackState;
    private EnemyState glitchState;

    private void Awake()
    {
        base.Awake();

        idleState = new IdleState(this, fsm);
        chaseState = new ChaseState(this, fsm);
        attackState = new AttackState(this, fsm);
        glitchState = new GlitchState(this, fsm);  // 웃는 천사 기믹 전용

        fsm.ChangeState(idle);
    }

    protected override void Update()
    {
        fsm.Update(); 
    }

    public override void StartAction()
    {
        // 거리를 체크하고, 감지 시 지직-불끄기 효과
    }

    public override void Attack()
    {
        // 가까이 오면 공격 실행
    }
}
