using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel;

    public void ToggleOptionPanel()
    {
        if (OptionPanel != null)
        {
            //Debug.Log("1");
            OptionPanel.SetActive(!OptionPanel.activeSelf);
        }
    }
}
