using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartControlPanel : InteractableObject, IInteractable
{
    [Header("īƮ")]
    public Cart cart;

    [Header("īƮ���� ���� ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera cartMapCam;

    [Header("īƮ���� ���� ī�޶� ���� ���̾�")]
    [SerializeField] LayerMask cartMapCamLayerMask;

    [Header("ĳ���͸� ���� ī�޶� ���� ���̾�")]
    [SerializeField] LayerMask mainLayerMask;

    [Header("ĳ���͸� ���� ī�޶� ���� ���̾�")]
    [SerializeField] GameObject door;

    //���� ī�޶�
    private Camera mainCam;

    private bool isFinished = false;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        CheckCartState();
        if(cart.isGoal && !isFinished)
        {
            OpenDoor();
            isFinished = true;
        }
    }

    public string GetInteractionText()
    {
        string str = "'E'Ű�� ���� �����ϱ�";

        return str;
    }

    public string GetNameText()
    {
        return "īƮ ��ġ";
    }

    public void OnInteract()
    {
        cart.isControlling = true;
    }

    /// <summary>
    /// īƮ�� ���� ���� ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool IsControlling()
    {
        return cart.isControlling;
    }

    /// <summary>
    /// īƮ ���¿� ���� ī�޶� ����
    /// </summary>
    void CheckCartState()
    {

        if (IsControlling())
        {
            cartMapCam.Priority = 2000;
            mainCam.cullingMask = cartMapCamLayerMask;
        }
        else
        {
            cartMapCam.Priority = 10;
            mainCam.cullingMask = mainLayerMask;
        }
    }

    void OpenDoor()
    {
        door.transform.DOMove(door.transform.position + new Vector3(0, 4f, 0), 10f); 
    }
}
