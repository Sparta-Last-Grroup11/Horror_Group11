using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHingeDoor : OpenableObject
{
    public float openVelocity = 90f;
    public float closeVelocity = -90f;
    public float motorForce = 100f;
    public float stopThreshold = 0.5f;

    private HingeJoint hinge;
    private JointMotor motor;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useMotor = false;
        motor = hinge.motor;

        JointLimits limits = hinge.limits;
        limits.min = -90f;     // 닫힌 상태
        limits.max = 0f;    // 열린 상태
        hinge.limits = limits;
    }
    protected override IEnumerator OpenRoutine()
    {
        motor.targetVelocity = openVelocity;
        motor.force = motorForce;
        hinge.motor = motor;
        hinge.useMotor = true;

        // 문이 열릴 때까지 대기
        yield return new WaitForSeconds(1f);

        hinge.useMotor = false;
        GameManager.Instance.SurfaceUpdate();
    }

    protected override IEnumerator CloseRoutine()
    {
        motor.targetVelocity = closeVelocity;
        motor.force = motorForce;
        hinge.motor = motor;
        hinge.useMotor = true;

        // 문이 닫힐 때까지 대기
        yield return new WaitUntil(() => Mathf.Abs(hinge.velocity) < stopThreshold);

        hinge.useMotor = false;
        GameManager.Instance.SurfaceUpdate();
    }
}