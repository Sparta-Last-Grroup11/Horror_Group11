using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMan : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        fsm = new SnailManFSM(this);
    }

    public override void StartAction()
    {
        Debug.Log("SnailMan: StartAction - chasing player");
        // TODO: 애니메이션, 사운드, NavMesh 이동 등
    }

    public override void Attack()
    {
        Debug.Log("SnailMan: Attack (if used)");
        // Snail은 기본적으로 공격 없거나 느린 타격 구현
    }

    // 상태 전이용 판단 메서드들
    public bool IsPlayerNear() { /* TODO: 거리 체크 */ return false; }
    public bool IsPlayerFar() { /* TODO: 거리 체크 */ return false; }
    public bool HasFinishedAttack() { /* TODO */ return true; }
}
