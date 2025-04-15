using UnityEngine;
using UnityEngine.InputSystem;

public class P_Interaction : PlayerInputController
{
    // Components
    public GameObject curInteractGameObject;
    private I_Interactable curInteractable;

    [Header("Ray")]
    [SerializeField] private float maxCheckDistance;
    [SerializeField] private LayerMask layerMask;

    public override void Awake()
    {
        base.Awake();
        interactAction.started += OnInteractStarted;
    }

    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxCheckDistance, Color.yellow);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<I_Interactable>();
                // 여기에 상호작용 UI 뜨게 하면 됨.
            }
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
        }
    }

    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        if (curInteractGameObject != null)
        {
            if (curInteractGameObject == null || curInteractable == null) return;
            curInteractable.OnInteraction();
        }
    }
}
