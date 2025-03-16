using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

enum ABNORMAL
{
    INETENSITY,
    GHOSTLIGHT,

}


public class PlayerStress : MonoBehaviour
{
    [SerializeField] private float stress;

    private float CCTVStress;

    private bool level1;
    private bool level2;


    [SerializeField] private Light[] stressLight;

    // Start is called before the first frame update
    void Start()
    {
        CCTVStress = 5;
        level1 = level2 = false;
    }

    public void GetStress(float amount)
    {
        stress += amount;

        if(stress>30&&!level1)
        {
            SetStressLevel(1);
        }
        else if(stress>70&&!level2)
        {
            SetStressLevel(2);
        }
    }

    private void SetStressLevel(int level)
    {
        AddAbnormal((ABNORMAL)level);
    }

    private void AddAbnormal(ABNORMAL situation)
    {
        switch (situation)
        {
            case ABNORMAL.INETENSITY:
                level1 = true;
                break;
            case ABNORMAL.GHOSTLIGHT:
                level2 = true;
                foreach (Light light in stressLight)
                {
                    light.enabled = true;
                }
                break;

        }
    }

    private IEnumerator SensedCCTV()
    {
        while (true)
        {
            stress += CCTVStress * Time.deltaTime;
            yield return null;
        }
    }

}
