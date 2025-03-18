using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneLoadObject : MonoBehaviour
{
    public string stageName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneLoader.Instance.LoadScene(stageName);
        }
    }
}
