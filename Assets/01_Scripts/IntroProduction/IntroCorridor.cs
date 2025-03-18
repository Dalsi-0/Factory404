using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCorridor : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private Transform spawnPoint;
    private bool isActived = false;

    /// <summary>
    /// 메인화면에서 움직이는 캐릭터와 충돌이 있을때 동적 네비메쉬 작동
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IntroAgent") && !isActived)
        {
            isActived = true;
            other.GetComponent<IntroAgent>().PassivityUpdateNavmesh();

            StartCoroutine(WaitOneFrame(other));
        }
    }

    /// <summary>
    /// 다음 통로 생성, 이전 통로 삭제, 현재 통로 지정, 움직이는 캐릭터의 목표지점 설정
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    IEnumerator WaitOneFrame(Collider other)
    {
        yield return null; // 동적 네비메쉬 제작 시간 기다리기

        // 다음 통로 생성, 이전 통로 삭제
        IntroCorridorSpawner.Instance.SpawnerNewCorridors(spawnPoint);

        // 현재 통로 지정
        IntroCorridorSpawner.Instance.SetCurrentCorridor(gameObject);

        // 움직이는 캐릭터의 목표지점 설정
        other.GetComponent<IntroAgent>().SetNewDestination(destination.position);
    }
}
