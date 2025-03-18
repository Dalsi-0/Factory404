using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetailUI : MonoBehaviour
{
    public GameObject InventoryUI;

    private ItemData item;

    [SerializeField] private Camera uiCamera;

    public Transform itemPivot;

    public float rotationSpeed;
    private bool isDrag;
    private GameObject hitObject;

    private float targetFOV;
    private float curFOV;
    private float smoothVelocity;
    public float smoothTime;

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    void Update()
    {
        ItemRotate();
        ItemZoom();
        ExitDetail();
    }

    private void OnEnable()
    {
        ItemSpawn();
        curFOV = 60f;
        targetFOV = curFOV;
        GameManager.Instance.Player.GetComponent<PlayerInput>().enabled = false;
        uiCamera.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.GetComponent<PlayerInput>().enabled = true;
        uiCamera.transform.GetChild(0).gameObject.SetActive(false);
    }

    /// <summary>
    /// Ŭ���� �������� �巡�׸� ���� ȸ����ų �� �ִ� ���
    /// </summary>
    private void ItemRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = uiCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hitObject = hit.collider.gameObject;
                isDrag = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            hitObject = null;
            isDrag = false;
        }

        if (isDrag)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            hitObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
            hitObject.transform.Rotate(Vector3.right, mouseY, Space.World);
        }
    }


    /// <summary>
    /// ī�޶��� FOV�� ������ �������� Ȯ�� �� ����� �� �ִ� ���
    /// </summary>
    private void ItemZoom()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScroll != 0)
        {
            targetFOV -= mouseScroll * zoomSpeed;
            targetFOV = Mathf.Clamp(targetFOV, minZoom, maxZoom);
        }

        curFOV = Mathf.SmoothDamp(curFOV, targetFOV, ref smoothVelocity, smoothTime);
        uiCamera.fieldOfView = curFOV;
    }

    /// <summary>
    /// �κ��丮 â���� �������� ������ �Ѱܹ޴� �Լ�
    /// </summary>
    /// <param name="item"> -> �κ��丮 â�� selectedItem </param>
    public void GetItemInfo(ItemData item)
    {
        this.item = item;
    }

    /// <summary>
    /// ������â�� ���� �� �κ��丮 â���� ���� ������ ������ ���� �������� ����
    /// </summary>
    private void ItemSpawn()
    {
        GameObject detailItem = Instantiate(item.detailPrefab, itemPivot);
    }

    /// <summary>
    /// ESC Ű�� ���� ������ â�� ����
    /// </summary>
    private void ExitDetail()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(itemPivot.GetChild(0).gameObject);
            this.gameObject.SetActive(false);
            InventoryUI.SetActive(true);
        }
    }
}
