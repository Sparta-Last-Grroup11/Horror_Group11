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

    public class AngelIdleState : EnemyState  // 기본 상태일 때 
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

            if (angel.CanSeePlayerFace() && angel.IsPlayerLookingAtMe())   // 1) 웃는천사가 플레이어를 볼 수 있는 상태(어둠), 2) 웃는천사가 플레이어와 마주쳤을 때  
            {
                fsm.ChangeState(new AngelChaseState(angel, fsm));  // 2) 추적Chase 상태로 전환
            }
        }
    }

    public class AngelChaseState : EnemyState    // 플레이어를 추격하는 상태일 때
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

            if (!angel.IsPlayerInRoom())  // 플레이어가 방 밖으로 나갈 때(즉 복도일 때)
            {
                if (angel.IsNearPlayer())  // 천사가 일정 거리 안에 있다면
                {
                    fsm.ChangeState(new AngelAttackState(angel, fsm));  // 공격 상태로 전환
                    return;
                }
            }
            else // 방 안에서 대기중일 때는
            {
                if (chaseTimer >= maxChaseTime)  // 일정 시간이 지나면 
                {
                    fsm.ChangeState(new AngelIdleState(angel, fsm));  // 추격 상태 풀리고 Idle 상태로 전환
                    return;
                }
            }

            if (angel.IsPlayerLookingAtMe())  // 플레이어와 보고 있을 때 
            {
                fsm.ChangeState(new AngelGlitchState(angel, fsm, this));  // 지직거리는 글리치 상태로 전환
            }
        }
    }

    public class AngelAttackState : EnemyState  // 플레이어를 공격하는 상태
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

    public class AngelGlitchState : EnemyState   // 적을 바라볼 때 글리치가 일어나는 상태
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
            // ex) UiManger.instance.PlayGlitch();  // 글리치 효과 UI(이펙트, 소리 등) 이 위치에 들어감 
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
            // 플레이어가 쳐다보지 않을 때, Glitch 효과 없어지도록 넣어주면 될 듯. 
        }
    }
}
