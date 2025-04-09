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
        // 1. Y축 회전 (Yaw) - Player 기준
        float yaw = playerLook.eulerAngles.y;

        // 2. X축 회전 (Pitch) - Camera 기준
        float pitch = cameraLook.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        // 3. 목표 회전 조합
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);

        // 4. 부드럽게 회전
        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, followSpeed * Time.deltaTime);
        transform.rotation = currentRotation;
    }
}
