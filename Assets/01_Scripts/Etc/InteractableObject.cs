using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private QuickOutline outline;

    private void Start()
    {
        outline = GetComponent<QuickOutline>();
    }

    public void OnOutline()
    {
        outline.enabled = true;
    }

    public void OffOutline()
    {
        outline.enabled = false;
    }
}
