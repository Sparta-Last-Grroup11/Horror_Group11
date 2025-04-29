using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI3D : BaseUI
{
    [Header("View Distance")]
    [SerializeField] private float zMaxDistance;
    [SerializeField] private float scrollSpeed;
    Vector3 objOriginalPos;
    GameObject curObj;

    [Header("Y Moving")]
    [SerializeField] private float yMaxDistance;
    [SerializeField] private float MoveSpeed;

    [Header("Rotate")]
    public float rotationSpeed = 20f;

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    public void Init(GameObject prefab, string description = "")
    {
        UIManager.Instance.IsUiActing = true;
        var canvas = UIManager.Instance.mainCanvas;
        var subCam = UIManager.Instance.subCam;

        curObj = UIManager.Instance.MakePrefabInSubCam(prefab);
        objOriginalPos = curObj.transform.localPosition;
        curObj.GetComponent<ItemOnUI>().Init(description);
    }

    protected override void Start()
    {
        base.Start();
        if (UIManager.Instance.CurUI3D != null)
            UIManager.Instance.CurUI3D.DestroySelf();
        UIManager.Instance.CurUI3D = this;
    }

    //전에 있던 DestroyUIObject에서 그냥 DestroySelf로 파괴하시면 됩니다.
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (curObj != null)
        {
            UIManager.Instance.RemovePrefabInSumCam();
            curObj = null;
        }
    }

    void Update()
    {
        if (curObj == null)
            return;
        Moving();
        Scrolling();
        Rotate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DestroySelf();
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

    void Rotate()
    {
        // 마우스 왼쪽 버튼을 누르면 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // 마우스 버튼 떼면 드래그 종료
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때 회전 처리
        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;

            curObj.transform.Rotate(0, rotationY, 0f, Space.Self);

            lastMousePosition = Input.mousePosition;
        }
    }

    public override void DestroySelf()
    {
        UIManager.Instance.IsUiActing = false;
        UIManager.Instance.CurUI3D = null;
        base.DestroySelf();
    }
}
