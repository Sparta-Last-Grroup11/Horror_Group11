using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInventoryUI : PopupUI
{
    [SerializeField] private RawImage image;
    [SerializeField] private TextMeshProUGUI inventoryTitle;
    [SerializeField] private GameObject itemSlotGroup;
    [SerializeField] private List<Slot> slots; // 갖고 있는 아이템으로 변경 필요
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image manualImage; // 조작법 이미지
    [Header("Rotate")]
    public float rotationSpeed = 20f;

    private bool isDragging = false;
    private Vector3 lastMousePosition;
    GameObject cur3DObject;

    protected override void Start()
    {
        base.Start();
        UIManager.Instance.IsUiActing = true;
        for (int i = 0; i < slots.Count; i++)
        {
            /*
            slots[i].transform.SetParent(itemSlotGroup.transform);
            slots[i].transform.SetAsLastSibling();
            */
            var slot = slots[i];
            slot.TryGetComponent<Button>(out Button but);
            if (but != null)
                but.onClick.AddListener(() => {
                    if (UIManager.Instance.cur3DObject != null)
                        UIManager.Instance.RemovePrefabInSumCam();
                    cur3DObject = UIManager.Instance.MakePrefabInSubCam(slot.Object3D); // 이제 안전!
                });
        }
    }

    protected void Update()
    {
        Rotate();
    }

    protected override void OnDestroy()
    {
        UIManager.Instance.RemovePrefabInSumCam();
    }

    void Rotate()
    {
        if (cur3DObject == null)
            return;
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

            cur3DObject.transform.Rotate(0, rotationY, 0f, Space.Self);

            lastMousePosition = Input.mousePosition;
        }
    }
}
