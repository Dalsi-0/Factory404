using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecrityCam : MonoBehaviour
{
    public GameObject camLight;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(camLight.transform.DOLocalRotate(new Vector3(20, -330, 0), 5f).SetEase(Ease.Linear));
        sequence.AppendInterval(2f); 
        sequence.Append(camLight.transform.DOLocalRotate(new Vector3(20, -210, 0), 5f).SetEase(Ease.Linear));
        sequence.AppendInterval(2f);
        sequence.SetLoops(-1);
        sequence.Play();
    }
}
