using UnityEngine;
using UnityEngine.InputSystem;

public class P_Interaction : MonoBehaviour
{
    public GameObject curInteractGameObject;
    private I_Interactable curInteractable;

    [Header("Ray")]
    [SerializeField] private float maxCheckDistance;
    [SerializeField] private LayerMask layerMask;

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

    public void InteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractGameObject != null)
        {
            // 상호작용 //Getcoponent
            if (curInteractGameObject == null || curInteractable == null)
                return;
            curInteractable.OnInteraction();
        }
    }
}
