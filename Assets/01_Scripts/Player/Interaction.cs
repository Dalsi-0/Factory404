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
    /// ����ĳ��Ʈ�� ���� ��ȣ�ۿ� ������ ������Ʈ�� ��ȣ�ۿ��� �غ� �ϴ� �Լ�
    /// ��ȣ�ۿ� ������ ������Ʈ�� ����ĳ��Ʈ�� ������ Outline�� ����� �̸��� ��ȣ�ۿ� ������ Ű�� UI�� ���� 
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
    /// ��ȣ�ۿ� ������ ������Ʈ�� �̸��� UI�� ���� �Լ�
    /// </summary>
    private void SetNameText()
    {
        nameText.gameObject.SetActive(true);
        nameText.text = curInteractable.GetNameText();
    }

    /// <summary>
    /// ��ȣ�ۿ� ������ Ű�� UI�� ���� �Լ�
    /// </summary>
    private void SetInteractionText()
    {
        interactionText.gameObject.SetActive(true);
        interactionText.text = curInteractable.GetInteractionText();
    }

    /// <summary>
    /// Input System�� ���� �޾� ��ȣ�ۿ� ������ ������Ʈ�� ��ȣ�ۿ��ϴ� �Լ�
    /// </summary>
    /// <param name="context"> -> ��ȣ�ۿ� �� Ű </param>
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
