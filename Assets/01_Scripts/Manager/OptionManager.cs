using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel; 

    public void ToggleOptionPanel() // �ɼ� �г� on/off ���
    {
        if (OptionPanel != null)
        {
            //Debug.Log("1");
            OptionPanel.SetActive(!OptionPanel.activeSelf);
        }
    }

    public void CloseOpionPanel()
    {
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(false);
        }
    }
}
