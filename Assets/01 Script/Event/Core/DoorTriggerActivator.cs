using UnityEngine;

public class DoorTriggerActivator : MonoBehaviour  // 플레이어가 문 근처 트리거에 들어오면, 관련 이벤트 1번만 활성화
{
    private bool isPlayerOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null && isPlayerOn == false)
        {
            isPlayerOn = true;

            // 스테이지 트리거(점프스케어 좀비) 활성화
            StageTriggerController.Instance.ActivateTriggers();

            // 체크포인트 저장 및 간호좀비 상태를 1층 스폰지점으로 설정
            GameManager.Instance.CheckPointSave(new Vector3(-1.43f, 1.88f, -0.30f));
            GameManager.Instance.SetNursePhase(NurseZombie.SpawnNursePhase.FirstFloor);

        }
    }

}
