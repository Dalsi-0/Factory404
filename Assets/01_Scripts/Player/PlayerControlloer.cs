using System;
using System.Collections;
using System.Collections.Generic;
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
    private bool isHaveFlash;
    private bool isOnFlash;

    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    public bool canLook = true;

    public Action inventory;
    public Action option;

    private Rigidbody _rigidbody;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        _rigidbody = GetComponent<Rigidbody>();
        animator=GetComponentInChildren<Animator>();
        isHaveFlash = true;
        isOnFlash = false;


        Cursor.lockState = CursorLockMode.Locked;

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
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool("IsMoving", false);
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
        if (context.phase == InputActionPhase.Started && isHaveFlash)
        {
            // flash On/Off
            isOnFlash = !isOnFlash;
            handLight.gameObject.SetActive(isOnFlash);
        }
    }

    #endregion

    /// <summary>
    /// 마우스 커서 보이게/안보이게
    /// </summary>
    private void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    /// <summary>
    /// 플래시 획득할 때 호출
    /// </summary>
    public void SetHaveFlash()
    {
        isHaveFlash = true;
    }
}
