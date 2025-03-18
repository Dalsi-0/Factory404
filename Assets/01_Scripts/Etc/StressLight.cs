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
    /// 불이 켜지면 플레이어 검색
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
    /// 내 주변에 플레이어가 있는지 검사
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
