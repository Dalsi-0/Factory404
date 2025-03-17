using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartControlPanel : InteractableObject, IInteractable
{
    [Header("카트")]
    public Cart cart;

    [Header("카트맵을 비출 카메라")]
    [SerializeField] private CinemachineVirtualCamera cartMapCam;

    [Header("카트맵을 비출 카메라가 찍을 레이어")]
    [SerializeField] LayerMask cartMapCamLayerMask;

    [Header("캐릭터를 비출 카메라가 찍을 레이어")]
    [SerializeField] LayerMask mainLayerMask;

    [Header("캐릭터를 비출 카메라가 찍을 레이어")]
    [SerializeField] GameObject door;

    //메인 카메라
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
        string str = "'E'키를 눌러 조종하기";

        return str;
    }

    public string GetNameText()
    {
        return "카트 장치";
    }

    public void OnInteract()
    {
        cart.isControlling = true;
    }

    /// <summary>
    /// 카트의 조종 상태 반환
    /// </summary>
    /// <returns></returns>
    public bool IsControlling()
    {
        return cart.isControlling;
    }

    /// <summary>
    /// 카트 상태에 따른 카메라 조절
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
