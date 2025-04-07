using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeLockPuzzle : MonoBehaviour
{
    [SerializeField] private int[] rightArray = {0,0,0,0};
    [SerializeField] private OpenSafeDoor door;
    [SerializeField] private AudioSource audio;

    private int count = 0;
 //   [HideInInspector]
    public int answer = 0;
    private int maxNumber = 9;
    private int arrayLength;
    public bool isLeft;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
        for (int i = 0; i < rightArray.Length; i++)
        {
            rightArray[i] = Random.Range(0,maxNumber+1);
            arrayLength = rightArray.Length ;
        }
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        RotateDial();
    }
    public void RotateDial()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isLeft)
            {
                answer = (answer + 1) % (maxNumber + 1);
                if (answer == rightArray[count])
                    audio.Play();
            }
            else
            {
                CheckAnswer();
                answer = (answer + 1) % (maxNumber + 1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (!isLeft)
            {
                answer = (answer + 1) % (maxNumber + 1);
                if (answer == rightArray[count])
                    audio.Play();
            }
            else
            {
                CheckAnswer();
                answer = (answer + 1) % (maxNumber + 1);
            }
        }
        if (count == arrayLength - 1 && answer == rightArray[arrayLength - 1])
        {
            SuccessOpen();
        }
    }

    void CheckAnswer()
    {
        if (answer == rightArray[count])
        {
            answer = 0;
            count++;
            isLeft = !isLeft;
        }
        else
        {
            answer = 0;
            count = 0;
            isLeft = false;
        }
    }

    void SuccessOpen()
    {
        door.OpenDoor();
        LockCursor();
        gameObject.SetActive(false);
    }
    void LockCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }   
}
