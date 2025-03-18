using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensingRange : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.stress.GetStress(Time.deltaTime * 10);
        }
    }
}
