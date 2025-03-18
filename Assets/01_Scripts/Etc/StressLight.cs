using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class StressLight : MonoBehaviour
{

    public float distance;
    public LayerMask playerLayer;

    private Light light;


    private void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
        CheckLight().Subscribe().AddTo(this);

    }
    
    /// <summary>
    /// ���� ������ �÷��̾� �˻�
    /// </summary>
    /// <returns></returns>
    private IObservable<Unit> CheckLight()
    {
        return Observable.ReturnUnit()
            .Where(_ => gameObject.activeSelf == true)
            .Do(_ =>
            {
                Observable.EveryUpdate().Subscribe(_ => CheckPlayer()).AddTo(this);
            });
    }


    /// <summary>
    /// �� �ֺ��� �÷��̾ �ִ��� �˻�
    /// </summary>
    private void CheckPlayer()
    {
        Collider[] collider = Physics.OverlapSphere(this.transform.position, distance, playerLayer);
        if (collider.Length > 0)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
}
