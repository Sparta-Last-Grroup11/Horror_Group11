using UnityEngine;

public class SkinLessZombie : Enemy   // 점프스케어 (플레이어 보면 빠르게 달려와서 깜놀시키고 사라짐, 무해함)
{
    public Animator skinLessZombieAnim;

    public float timer = 0f;  // 달려든 후 일정 시간 지나면 사라지게 만들 타이머
    public float rushSpeed = 30f;         // 달려드는 속도
    public float disappearTime = 1.5f;    // 사라지기까지 시간
    public float rushDelay = 0.5f; // 달려들기 전에 대기하는 시간

    protected override void Start()
    {
        base.Start();
        skinLessZombieAnim = GetComponentInChildren<Animator>();

        InitSkinLessFSM();

    }

    private void InitSkinLessFSM()
    {
        fsm = new EnemyStateMachine();
        fsm.ChangeState(new SkinLessZombieAmbushState(this, fsm));
    }

}
