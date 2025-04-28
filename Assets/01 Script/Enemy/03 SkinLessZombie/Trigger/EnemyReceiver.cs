using UnityEngine;

public class EnemyReceiver : Receiver
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogWarning($"[EnemyReceiver] {name}에 Enemy 컴포넌트가 없습니다.");
        }

    }

    public override void ReceiveTrigger()
    {
        enemy.TriggerEventEnemy();

    }
}
