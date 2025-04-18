using UnityEngine;

public class SafeLockPuzzle : ItemOnUI
{
    [Header("Puzzle Setting")]
    [SerializeField] private int[] rightArray = {0,0,0,0};
    [SerializeField] private int randomRange;
    [SerializeField]private int maxNumber = 9;

    [Header("References")]
    [SerializeField] private Transform dialTransform;
    [SerializeField] private AudioClip clip;
    [SerializeField] private Surprise surprise;


    private int rotateCount;
    public int answer;
    private int arrayLength;
    private bool isLeft;
    private float currentAngle;
    private float targetAngle;
    private float rotationSpeed = 36f; // degrees per second
    private bool isRotating;

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

    protected  void Update()
    {
        RotateDial();
        RotateDialMotion();
    }
    public void RotateDial() //다이얼 돌리기
    {
        if (isRotating) return;
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

        if (Random.Range(0, randomRange) == 0)
        {
            surprise.SurpriseSound();
        }

        if (rotateCount == arrayLength - 1 && answer == rightArray[arrayLength - 1]) // 마지막 암호까지 맞췄다면 문 열기
        {
            SuccessOpen();
        }
    }

    private void SetRotationDial(bool goingLeft)
    {
        float angleStep = 36f;
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
            rotateCount++;
        }
        else //일치하지 않는다면 전부 초기화
        {
            rotateCount = 0;
            targetAngle = 0f;
            isRotating = true;
        }
        answer = 0;

    }

    void SuccessOpen() // 비밀번호를 맞췄을 경우
    {
        Debug.Log("clear");
        PuzzleManager.Instance.GateHouseSaveDial.OpenSaveDoor();
        UIManager.Instance.CurUI3D.DestroySelf();
    }
}
