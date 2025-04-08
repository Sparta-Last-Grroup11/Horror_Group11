using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyFSM fsm;

    protected virtual void Awake()
    {
        fsm = new EnemyFSM();
    }

    protected virtual void Update()
    {
        fsm?.Update();
    }

    public virtual void StartAction()  // 몬스터가 추격 시작할 때 호출
    {
        // animator.SetTrigger("Run");
    }

    public virtual void Attack()  // 몬스터가 공격할 때 호출
    {
        // animator.SetTrigger("Attack");
    }
}
