using UnityEngine;

public class FlashLook : MonoBehaviour
{
    [SerializeField] private Transform playerLook;
    [SerializeField] private Transform cameraLook;
    [SerializeField] private float followSpeed = 5f;

    private Quaternion currentRotation;

    private void Start()
    {
        currentRotation = transform.rotation;
    }

    private void Update()
    {
        // Player 기준 (Y축 회전)
        /// 좌우 회전할 때 카메라는 고정이고 플레이어 몸이 움직이기 때문
        float yaw = playerLook.eulerAngles.y;

        // Camera 기준 (X축 회전)
        /// 위아래 회전할 때 플레이어 몸은 고정이고 카메라만 움직이기 때문
        float pitch = cameraLook.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        /// 따라서 손전등은 좌우는 플레이어의 축을 따라, 위아래는 카메라의 축을 따라 이동하게 만듦
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);

        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, followSpeed * Time.deltaTime);
        transform.rotation = currentRotation;
    }
}
