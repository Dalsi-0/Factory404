using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensingRange : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("°¨ÁöµÊ!");
        }
    }
}
