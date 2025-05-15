using System.Collections;
using UnityEngine;

public class SafeLockPuzzle : ItemOnUI
{
    [Header("Puzzle Setting")]
    [SerializeField] private int[] rightArray = {0,0,0,0};
    [SerializeField]private int maxNumber = 9;
    [SerializeField] private OpenPuzzle safe;
    [SerializeField] private Renderer[] checker;
    [Header("References")]
    [SerializeField] private Transform dialTransform;
    [SerializeField] private AudioClip clip;


    private int rotateCount;
    public int answer;
    private int arrayLength;
    private bool isLeft;
    private float currentAngle;
    private float targetAngle;
    [SerializeField] private float rotationSpeed = 36f; // degrees per second
    private bool isRotating = false;
    private bool isResetting = false;
    public override void Init(string description = "") //실행 시
    {
        for (int i = 0; i < rightArray.Length; i++) //암호 초기화
        {
            rightArray[i] = Random.Range(0,maxNumber+1);
        }

        arrayLength = rightArray.Length;
        rotateCount = 0;
        answer = 0;
        isLeft = false;
        currentAngle = 0f;
        isRotating = false;
    }

    public void SetSafe(OpenPuzzle puzzle) //금고 정보 저장
    {
        safe = puzzle;
    }

    protected  void Update()
    {
        RotateDial();
        RotateDialMotion();
    }

    public void RotateDial() //다이얼 돌리기
    {
        if (isRotating || isResetting) return;
        if (Input.GetKeyDown(KeyCode.A)) //왼쪽으로 회전
        {
            ProcessDialInput(true);
        }
        else if (Input.GetKeyDown(KeyCode.D)) //오른쪽으로 회전
        {
            ProcessDialInput(false);
        }
    }

    private void ProcessDialInput(bool goingLeft) //입력 방향 확인
    {
        if (goingLeft == isLeft) //방향 일치 시
        {
            answer = (answer + 1) % (maxNumber + 1);
            if(answer == rightArray[rotateCount])
            {
                AudioManager.Instance.Audio2DPlay(clip);
            }
        }
        else //방향 불일치 시
        {
            CheckAnswer(); //답과 일치하는지 확인
        }

        SetRotationDial(goingLeft); //다이얼 회전 설정

        isLeft = goingLeft; //회전방향 저장

        if (rotateCount == arrayLength - 1 && answer == rightArray[arrayLength - 1]) // 마지막 암호까지 맞췄다면 문 열기
        {
            SuccessOpen();
        }
    }

    private void SetRotationDial(bool goingLeft) //다이얼 회전 각도 설정
    {
        float angleStep = 360 / (maxNumber + 1);
        targetAngle += goingLeft ? -angleStep : angleStep;
        isRotating = true;
    }

    private void RotateDialMotion() //다이얼 회전 실행
    {
        if (!isRotating) return;

        currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        dialTransform.localEulerAngles = new Vector3(0,0, currentAngle);
        if(Mathf.Approximately(currentAngle, targetAngle))
        {
            isRotating = false;
        }
    }
    void CheckAnswer() //현재 입력이 암호와 일치한지
    {
        if (answer == rightArray[rotateCount])
        {
            checker[rotateCount].material.color = Color.green;
            rotateCount++;
            answer = (answer + 1) % (maxNumber + 1);
        }
        else //일치하지 않는다면 전부 초기화
        {
            for(int i = 0; i < rightArray.Length; i++)
            {
                checker[i].material.color = Color.red;
            }

            StartCoroutine(ResetDial());
        }
        answer = 0;

    }
    IEnumerator ResetDial() //다이얼 초기화
    {
        isResetting = true;

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < rightArray.Length; i++)
        {
            checker[i].material.color = Color.gray;
        }

        rotateCount = 0;
        answer = 0;
        targetAngle = 0f;
        isLeft = false;
        if (!Mathf.Approximately(currentAngle, targetAngle))
        {
            isRotating = true;

            while (!Mathf.Approximately(currentAngle, targetAngle))
            {
                yield return null;
            }
            isRotating = false;
        }
        
        isResetting = false;
    }
    void SuccessOpen() // 비밀번호를 맞췄을 경우
    {
        Debug.Log("clear");
        safe.OpenSaveDoor();
        UIManager.Instance.CurUI3D.DestroySelf();
    }
}
