using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerControlloer controlloer;
    public PlayerStress stress;
    public PlayerInput playerInput;
    public Interaction playerInteraction;


    public ItemData curItemData;
    public Action addItem;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.Player = this;
        controlloer = GetComponent<PlayerControlloer>();
        stress= GetComponent<PlayerStress>();
        playerInput = GetComponent<PlayerInput>();
        playerInteraction = GetComponent<Interaction>();
    }
}
