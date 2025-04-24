using UnityEngine;

public class EnemyReceiver : Receiver
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public override void ReceiveTrigger()
    {
        if (enemy != null)
        {
            enemy.isDoorOpened = true;
            Debug.Log("[ZombieReceiver] 문 열림 트리거 수신 -> isDoorOpened = true");
        }
    }
}
