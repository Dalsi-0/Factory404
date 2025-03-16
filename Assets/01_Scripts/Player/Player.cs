using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerControlloer controlloer;
    public PlayerStress stress;


    public ItemData curItemData;
    public Action addItem;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Player = this;
        controlloer = GetComponent<PlayerControlloer>();
        stress= GetComponent<PlayerStress>();
    }
}
