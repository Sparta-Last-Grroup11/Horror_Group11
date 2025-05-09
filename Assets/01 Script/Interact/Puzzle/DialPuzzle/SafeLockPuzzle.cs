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
    public override void Init(string description = "")
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

    public void SetSafe(OpenPuzzle puzzle)
    {
        safe = puzzle;
    }
    protected  void Update()
    {
        RotateDialMotion();
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

    private void ProcessDialInput(bool goingLeft)
    {
        bool sameDirection = goingLeft == isLeft;
        if (sameDirection)
        {
            answer = (answer + 1) % (maxNumber + 1);
            if(answer == rightArray[rotateCount])
            {
                AudioManager.Instance.Audio2DPlay(clip);
            }
        }
        else
        {
            CheckAnswer();
            answer = (answer + 1) % (maxNumber + 1);
        }

        SetRotationDial(goingLeft);

        isLeft = goingLeft;

        if (rotateCount == arrayLength - 1 && answer == rightArray[arrayLength - 1]) // 마지막 암호까지 맞췄다면 문 열기
        {
            SuccessOpen();
        }
    }

    private void SetRotationDial(bool goingLeft)
    {
        float angleStep = 360 / (maxNumber + 1);
        targetAngle += goingLeft ? -angleStep : angleStep;
        isRotating = true;
    }

    private void RotateDialMotion()
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
    IEnumerator ResetDial()
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
