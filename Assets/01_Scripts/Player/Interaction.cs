using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;
    private InteractableObject curInteractableObject;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI interactionText;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RayInteraction();
    }
    
    /// <summary>
    /// 레이캐스트를 통해 상호작용 가능한 오브젝트와 상호작용할 준비를 하는 함수
    /// 상호작용 가능한 오브젝트가 레이캐스트에 닿으면 Outline이 생기고 이름과 상호작용 가능한 키를 UI에 띄운다 
    /// </summary>
    private void RayInteraction()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();
                SetNameText();
                SetInteractionText();

                if (curInteractableObject != null)
                {
                    curInteractableObject.OffOutline();
                }
                curInteractableObject = hit.collider.GetComponent<InteractableObject>();
                curInteractableObject.OnOutline();
            }
        }
        else
        {
            if (curInteractableObject != null)
            {
                curInteractableObject.OffOutline();
            }

            curInteractGameObject = null;
            curInteractable = null;
            curInteractableObject = null;

            nameText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 상호작용 가능한 오브젝트의 이름을 UI에 띄우는 함수
    /// </summary>
    private void SetNameText()
    {
        nameText.gameObject.SetActive(true);
        nameText.text = curInteractable.GetNameText();
    }

    /// <summary>
    /// 상호작용 가능한 키를 UI에 띄우는 함수
    /// </summary>
    private void SetInteractionText()
    {
        interactionText.gameObject.SetActive(true);
        interactionText.text = curInteractable.GetInteractionText();
    }

    /// <summary>
    /// Input System의 값을 받아 상호작용 가능한 오브젝트와 상호작용하는 함수
    /// </summary>
    /// <param name="context"> -> 상호작용 할 키 </param>
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            nameText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(false);
        }
    }
}
