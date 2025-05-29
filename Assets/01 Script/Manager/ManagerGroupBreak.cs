using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//매니저 초기에 호출하면 자동 생성되게 만들어두지 않아서 따로 정리 해놨다가 모든 자식을 해제하는 데 쓰이던 스크립트(이제 안씀)
//Not Use.
public class ManagerGroupBreak : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject);
    }
}
