using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilingAngel : Enemy   // 웃는 천사 기믹
{
    protected override void Awake()
    {
        base.Awake();
        fsm = new SmilingAngelFSM(this);
    }

    public override void StartAction()
    {
        // 애니메이션, 사운드 넣어주면 됨
    }

    public override void Attack()
    {
        // 공격 애니메이션, 데미지 적용 등
    }

    public bool CanSeePlayerFace() 
    { 
        /* TODO */ return false; 
    }
    
    public bool IsPlayerLookingAtMe() 
    { 
        /* TODO */ return false; 
    }
    
    public bool IsPlayerInRoom() 
    { 
        /* TODO */ return true; 
    }

    public bool IsNearPlayer() 
    { 
        /* TODO */ return false; 
    }
    
    public bool IsLightOn() 
    { 
        /* TODO */ return false; 
    }
    
    public bool HasFinishedAttack() 
    { 
        /* TODO */ return true; 
    }
    
    public bool IsPlayerLookingAway() 
    { /* TODO */ return false; }
    
    public bool ChaseTimeExceeded() // 추격하던 적이 일정 시간 되면 추격 종료
    { /* TODO */ return false; }

}
