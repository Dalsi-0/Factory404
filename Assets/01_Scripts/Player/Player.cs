using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerControlloer controlloer;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Player = this;
        controlloer = GetComponent<PlayerControlloer>();
    }
}
