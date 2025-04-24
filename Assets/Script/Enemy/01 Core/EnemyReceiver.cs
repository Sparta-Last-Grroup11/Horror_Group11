using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyReceiver : Receiver
{
    private Enemy enemy;
    private SkinLessZombie skinLessZombie;
    [SerializeField] private EventTrigger trigger;

    public void Init(SkinLessZombie zombie, Enemy linkedEnemy = null)
    {
        skinLessZombie = zombie;
        enemy = linkedEnemy;
    }

    private void Start()
    {
        skinLessZombie = GetComponent<SkinLessZombie>();

    }

    private void Awake()
    {
        // 맵에다 따로 깔아서 테스트
        trigger = TriggerManager.Instance.triggers[0];  
        trigger.AddReceiver(this);
    }

    public override void ReceiveTrigger()
    {
        if (enemy != null)
        {
            enemy.isDoorOpened = true;
        }

        if (skinLessZombie != null)
        {
            skinLessZombie.TriggerAmbush();
        }

    }
}
