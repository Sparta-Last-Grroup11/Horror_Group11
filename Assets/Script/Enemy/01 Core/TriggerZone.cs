using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public EnemyReceiver receiver;

    public void AssignZombie (SkinLessZombie zombie)
    {
        if (receiver !=  null)
        {
            receiver.Init(zombie);
        }
    }
}
