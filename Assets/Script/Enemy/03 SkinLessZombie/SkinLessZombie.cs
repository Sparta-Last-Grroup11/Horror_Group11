using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkinLessZombie : Enemy   // 스네일맨 기믹
{
    public Animator snailManAnimator { get; private set; }
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 5f;
    public float detectionRange = 5f;

    public Transform[] patrolPoints;
    public Vector3 originalPosition { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        snailManAnimator = GetComponentInChildren<Animator>();
        originalPosition = transform.position;
    }

    protected override void Start()
    {
        fsm.ChangeState(new Snail_PatrolState(this, fsm, patrolPoints));
    }

    public void MoveTo(Vector3 pos)
    {
        Agent.SetDestination(pos);
        snailManAnimator.SetFloat("Speed", Agent.Speed);
    }

    // 상태 전이용 판단 메서드들
    public bool IsPlayerNear() { /* TODO: 거리 체크 */ return false; }
    public bool IsPlayerFar() { /* TODO: 거리 체크 */ return false; }

}
