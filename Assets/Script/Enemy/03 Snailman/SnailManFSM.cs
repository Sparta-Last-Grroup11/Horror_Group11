using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailManFSM : EnemyFSM
{
    public SnailManFSM(SkinLessZombie snail)
    {
        // 초기 상태 설정
        ChangeState(new SnailIdleState(snail, this));
    }

    public class SnailIdleState : EnemyState
    {
        private SkinLessZombie snail;

        public SnailIdleState(Enemy enemy, EnemyFSM fsm) : base(enemy, fsm)
        {
            snail = enemy as SkinLessZombie;
        }

        public override void Enter()
        {
            // TODO: Idle 애니메이션 트리거
        }

        public override void Update()
        {
            if (snail.IsPlayerNear())
            {
                fsm.ChangeState(new SnailChaseState(snail, fsm));
            }
        }
    }

    public class SnailChaseState : EnemyState
    {
        private SkinLessZombie snail;

        public SnailChaseState(Enemy enemy, EnemyFSM fsm) : base(enemy, fsm)
        {
            snail = enemy as SkinLessZombie;
        }

        public override void Enter()
        {
            snail.StartAction();
        }

        public override void Update()
        {
            // TODO: 플레이어 위치로 이동 로직

            // 플레이어가 멀어지면 Idle로
            if (snail.IsPlayerFar())
            {
                fsm.ChangeState(new SnailIdleState(snail, fsm));
            }
        }
    }
}
