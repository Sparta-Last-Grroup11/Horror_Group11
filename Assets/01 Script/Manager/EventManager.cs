using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public bool IsEventRun;
    protected override bool dontDestroy => false;
}
