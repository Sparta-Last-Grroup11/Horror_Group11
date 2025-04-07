using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private Enemy enemy;
    private EnemyFSM fsm;

    public ChaseState(Enemy enemy, EnemyFSM fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter() 
    { 

    }
    
    public void Execute() 
    {
        
    }
    
    public void Exit() 
    {
        
    }
}
