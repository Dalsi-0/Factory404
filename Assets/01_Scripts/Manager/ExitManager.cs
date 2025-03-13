using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit(); 
    }
}
