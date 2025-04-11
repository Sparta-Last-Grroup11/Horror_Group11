using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DInterface : MonoBehaviour
{
    [Header("View Distance")]
    [SerializeField] private float zMaxDistance;
    [SerializeField] private float scrollSpeed;
    Camera sub;
    Vector3 objOriginalPos;
    GameObject curObj;

    [Header("Y Moving")]
    [SerializeField] private float yMaxDistance;
    [SerializeField] private float MoveSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        curObj = UIManager.Instance.UI3DManager.CurGameObject;
        objOriginalPos = curObj.transform.localPosition;
        sub = GameManager.Instance.subCam;
    }

    void Update()
    {
        Moving();
        Scrolling();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.subCam.transform.position = objOriginalPos;
            UIManager.Instance.UI3DManager.DestroyUIObject();
            UIManager.Instance.IsUiActing = false;
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(gameObject);
        }
    }

    void Moving()
    {
        //상하 입력에 따라 이동
        float verticalInput = Input.GetAxis("Vertical"); // W/S 또는 ↑/↓ 키

        if (Mathf.Abs(verticalInput) > 0.01f)
        {
            Vector3 objPos = curObj.transform.localPosition;
            Vector3 newPos = objPos + Vector3.up * -verticalInput * MoveSpeed * Time.deltaTime;

            float yDistance = Mathf.Abs(newPos.y - objOriginalPos.y);

            if (yDistance <= yMaxDistance)
            {
                curObj.transform.localPosition = newPos;
            }
        }
    }
    
    void Scrolling()
    {
        //스크롤에 따른 오브젝트 전후방 이동
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 objPos = curObj.transform.localPosition;
            Vector3 dir = Vector3.forward;

            Vector3 newPos = objPos + dir * -scroll * scrollSpeed * Time.deltaTime;

            float distance = Mathf.Abs(objOriginalPos.z - newPos.z);

            if (distance <= zMaxDistance)
            {
                curObj.transform.localPosition = newPos;
            }
        }
    }
}
