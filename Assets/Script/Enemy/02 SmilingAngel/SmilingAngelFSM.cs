using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilingAngelFSM : EnemyFSM
{
    public SmilingAngelFSM(SmilingAngel angel)
    {
        // 초기 상태 설정
        ChangeState(new AngelIdleState(angel, this));
    }

    public class AngelIdleState : EnemyState
    {
        private SmilingAngel angel;

        public AngelIdleState(Enemy enemy, EnemyFSM fsm) : base(enemy, fsm)
        {
            angel = enemy as SmilingAngel;
        }

        public override void Enter()
        {
            // TODO: Idle 애니메이션 트리거
        }

        public override void Update()
        {
            if (angel.IsLightOn())
                return;

            if (angel.CanSeePlayerFace() && angel.IsPlayerLookingAtMe())
            {
                fsm.ChangeState(new AngelChaseState(angel, fsm));
            }
        }
    }

    public class AngelChaseState : EnemyState
    {
        private SmilingAngel angel;
        private float chaseTimer;
        private const float maxChaseTime = 5f;

        public AngelChaseState(Enemy enemy, EnemyFSM fsm) : base(enemy, fsm)
        {
            angel = enemy as SmilingAngel;
        }

        public override void Enter()
        {
            chaseTimer = 0f;
            angel.StartAction();
        }

        public override void Update()
        {
            if (angel.IsLightOn())
                return;

            chaseTimer += Time.deltaTime;

            // 복도에서 추격
            if (!angel.IsPlayerInRoom())
            {
                if (angel.IsNearPlayer())
                {
                    fsm.ChangeState(new AngelAttackState(angel, fsm));
                    return;
                }
            }
            else // 방 안에서 대기
            {
                if (chaseTimer >= maxChaseTime)
                {
                    fsm.ChangeState(new AngelIdleState(angel, fsm));
                    return;
                }
            }

            // Any State: Glitch 전이
            if (angel.IsPlayerLookingAtMe())
            {
                fsm.ChangeState(new AngelGlitchState(angel, fsm, this));
            }
        }
    }

    public class AngelAttackState : EnemyState
    {
        private SmilingAngel angel;

        public AngelAttackState(Enemy enemy, EnemyFSM fsm) : base(enemy, fsm)
        {
            angel = enemy as SmilingAngel;
        }

        public override void Enter()
        {
            angel.Attack();
        }

        public override void Update()
        {
            if (angel.HasFinishedAttack())
            {
                fsm.ChangeState(new AngelIdleState(angel, fsm));
            }
        }
    }

    public class AngelGlitchState : EnemyState
    {
        private SmilingAngel angel;
        private EnemyState previousState;

        public AngelGlitchState(Enemy enemy, EnemyFSM fsm, EnemyState prevState) : base(enemy, fsm)
        {
            angel = enemy as SmilingAngel;
            previousState = prevState;
        }

        public override void Enter()
        {
            // TODO: Glitch 효과 (이펙트, 소리 등)
        }

        public override void Update()
        {
            if (angel.IsPlayerLookingAway())
            {
                fsm.ChangeState(previousState);
            }
        }

        public override void Exit()
        {
            // TODO: Glitch 효과 정리
        }
    }
}
