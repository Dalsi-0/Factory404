using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlloer : MonoBehaviour
{

    private GameManager gameManager;


    [Header("Movement")]
    public float moveSpeed;
    public float jumpPow;
    private Vector2 curMovementInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;

    [Header("Flash")]
    [SerializeField]private GameObject flash;
    [SerializeField]private Light handLight;
    private bool isOnFlash;

    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    public bool canLook = true;

    public Action inventory;
    public Action option;

    private InventoryUI inventoryUI;

    private Rigidbody _rigidbody;

    private Animator animator;

    CinemachineVirtualCamera _camera;
    CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        gameManager = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody>();
        animator=GetComponentInChildren<Animator>();
        _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        inventoryUI = GameObject.Find("UI/Inventory/InventoryUI").GetComponent<InventoryUI>();
        isOnFlash = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        lookSensitivity = OptionManager.Instance.mouseSensitivitySlider.value;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    /// <summary>
    /// 실제 이동 부분
    /// </summary>
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.normal != Vector3.up)
            {
                dir = Vector3.ProjectOnPlane(dir, hit.normal).normalized;
            }
        } 
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    /// <summary>
    /// 카메라 시점 회전 및 캐릭터 회전
    /// </summary>
    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// 인풋시스템 관련
    /// </summary>
    /// <param name="context"></param>
    #region InputAction
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            animator.SetBool("IsMoving", true);
            noise.m_AmplitudeGain = 0.5f;
            noise.m_FrequencyGain = 0.05f;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool("IsMoving", false);
            noise.m_AmplitudeGain = 0.5f;
            noise.m_FrequencyGain = 0.01f;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (_rigidbody.velocity == Vector3.zero || context.phase == InputActionPhase.Canceled)
        {
            animator.SetBool("IsRun", false);
            moveSpeed = 3f;
            noise.m_AmplitudeGain = 0.5f;
            noise.m_FrequencyGain = 0.05f;
            return;
        }
        else if(context.phase==InputActionPhase.Performed)
        {
            animator.SetBool("IsRun", true);
            moveSpeed = 5f;
            noise.m_AmplitudeGain = 1f;
            noise.m_FrequencyGain = 0.1f;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            option?.Invoke();
            ToggleCursor();
        }
    }

    public void OnFlash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && HaveFlash())
        {
            isOnFlash = !isOnFlash;
            handLight.gameObject.SetActive(isOnFlash);
        }
    }

    #endregion

    public bool HaveFlash()
    {
        if (inventoryUI.FindItem("손전등"))
        {
            flash.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 마우스 커서 보이게/안보이게
    /// </summary>
    private void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
