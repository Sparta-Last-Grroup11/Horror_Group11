using System.Collections;
using System.Collections.Generic;
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
        enemy.TriggerEventEnemy();

    }
}
