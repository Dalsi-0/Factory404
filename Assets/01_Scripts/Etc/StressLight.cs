using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressLight : MonoBehaviour
{

    public float distance;
    public LayerMask playerLayer;

    private Light light;


    private void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        Collider[] collider = Physics.OverlapSphere(this.transform.position, distance, playerLayer);
        if(collider.Length>0)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
}
