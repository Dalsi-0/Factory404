using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

enum ABNORMAL
{
    NONE,
    INETENSITY,
    GHOSTLIGHT,

}


public class PlayerStress : MonoBehaviour
{
    //[SerializeField] private float stress;

    public float level1Value;
    public float level2Value;

    private IntReactiveProperty nowStressLevel;

    private float CCTVStress;

    // 위치 이동 필요
    [SerializeField] private Light[] stressLight;

    Image slider;

    private FloatReactiveProperty stress;

    // Start is called before the first frame update
    void Start()
    {
        CCTVStress = 5;
        nowStressLevel.Value = 0;
        stress.DistinctUntilChanged().Select(val=> s.fillAmount = val).Subscribe();
        nowStressLevel.Distinct().Subscribe(val => SetStressLevel(val));
    }

    public void GetStress(float amount)
    {
        stress.Value += amount;
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
                // 카메라 시야 줄이기 -> Volume 호출하는법?
                break;
            case ABNORMAL.GHOSTLIGHT:
                // 불끄기 -> 라이트 목록 다른곳에 두고 호출만 하기
                foreach (Light light in stressLight)
                {
                    light.GetComponent<StressLight>().enabled = true;
                }
                break;

        }
    }

}
