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

    // ��ġ �̵� �ʿ�
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
                // ī�޶� �þ� ���̱� -> Volume ȣ���ϴ¹�?
                break;
            case ABNORMAL.GHOSTLIGHT:
                // �Ҳ��� -> ����Ʈ ��� �ٸ����� �ΰ� ȣ�⸸ �ϱ�
                foreach (Light light in stressLight)
                {
                    light.GetComponent<StressLight>().enabled = true;
                }
                break;

        }
    }

}
