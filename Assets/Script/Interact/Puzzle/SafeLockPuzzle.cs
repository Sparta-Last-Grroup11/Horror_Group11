using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeLockPuzzle : ItemOnUI
{
    [SerializeField] private int[] rightArray = {0,0,0,0};
    [SerializeField] private ControlDoor door;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Surprise surprise;
    [SerializeField] private int randomRange;

    private int rotateCount = 0;
    public int answer = 0;
    private int maxNumber = 9;
    private int arrayLength;
    public bool isLeft;

    public override void Init(string input = "")
    {
        audio = GetComponent<AudioSource>();
        for (int i = 0; i < rightArray.Length; i++) //암호 초기화
        {
            rightArray[i] = Random.Range(0,maxNumber+1);
            arrayLength = rightArray.Length ;
        }
    }

    protected override void Update()
    {
        base.Update();
        RotateDial();
    }
    public void RotateDial() //다이얼 돌리기
    {
        if (Input.GetKeyDown(KeyCode.A)) //왼쪽으로 회전
        {
            if (isLeft) //직전에 왼쪽으로 회전시켰을 때
            {
                answer = (answer + 1) % (maxNumber + 1);
                if (answer == rightArray[rotateCount])
                    audio.Play();
            }
            else  //직전에 오른쪽으로 돌렸다면
            {
                CheckAnswer(); //돌린 횟수가 암호와 일치하는지 확인
                answer = (answer + 1) % (maxNumber + 1);
            }
            if (Random.Range(0, randomRange) == 0)
            {
                surprise.SurpriseSound();
            }
        }
        else if(Input.GetKeyDown(KeyCode.D)) //오른쪽으로 회전
        {
            if (!isLeft) //직전에 오른쪽으로 회전했을 때
            {
                answer = (answer + 1) % (maxNumber + 1);
                if (answer == rightArray[rotateCount])
                    audio.Play();
            }
            else //직전에 왼쪽으로 회전했을 때
            {
                CheckAnswer(); //돌린 횟수가 암호와 일치하는지 확인
                answer = (answer + 1) % (maxNumber + 1);
            }
            if (Random.Range(0, randomRange) == 0)
            {
                surprise.SurpriseSound();
            }
        }
        if (rotateCount == arrayLength - 1 && answer == rightArray[arrayLength - 1]) // 마지막 암호까지 맞췄다면 문 열기
        {
            SuccessOpen();
        }
    }

    void CheckAnswer() //현재 입력이 암호와 일치한지
    {
        if (answer == rightArray[rotateCount])
        {
            rotateCount++;
            isLeft = !isLeft;
        }
        else //일치하지 않는다면 전부 초기화
        {
            rotateCount = 0;
            isLeft = false;
        }
        answer = 0;
    }

    void SuccessOpen() // 비밀번호를 맞췄을 경우
    {
        LockCursor();
        PuzzleManager.Instance.GateHouseSaveDial.OpenSaveDoor();
        UIManager.Instance.UI3DManager.ui3SInterface.CloseUI();
        Destroy(gameObject);
    }
    void LockCursor() //커서 고정 해제
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }   
}
