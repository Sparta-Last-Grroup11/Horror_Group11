using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public virtual void StartAction()
    {
        // 몬스터가 움직이기 시작할 때 할 일 
    }

    public virtual void Attack()
    {
        // 몬스터가 공격할 때 할 일
    }
}
