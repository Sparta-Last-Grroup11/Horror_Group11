using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : PlayerInputController
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
                UIManager.Instance.Get<Interacting>().InteractOn();
            }
        }
        else
        {
            Interacting interacting = UIManager.Instance.Get<Interacting>();
            if (interacting != null)
            {
                interacting.InteractOff();
            }
            curInteractGameObject = null;
            curInteractable = null;
        }
    }

    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        if (curInteractGameObject != null)
        {
            if (curInteractGameObject == null || curInteractable == null || UIManager.Instance.IsUiActing) return;
            curInteractable.OnInteraction();
        }
    }

    private void OnDestroy()
    {
        interactAction.started -= OnInteractStarted;
    }
}
