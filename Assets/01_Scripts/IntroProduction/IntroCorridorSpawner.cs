using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCorridorSpawner : MonoBehaviour
{
    public static IntroCorridorSpawner Instance { get; private set; }

    [SerializeField] private Transform originPos;
    [SerializeField] private Transform introAgent;
    [SerializeField] private GameObject[] prefabsCorridors;

    private GameObject currentCorridor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RenderSettings.fog = true;
        currentCorridor = null;
        SpawnerNewCorridors(originPos);
        introAgent.position = originPos.position;
        ShuffleArray(prefabsCorridors);
    }

    public void SpawnerNewCorridors(Transform spawnPoint)
    {
        if (prefabsCorridors.Length == 0) return;

        ShuffleArray(prefabsCorridors);

        // 새로운 복도를 생성하기 전에 기존 복도 삭제
        if (currentCorridor != null)
        {
            Destroy(currentCorridor);
        }

        Instantiate(prefabsCorridors[0], spawnPoint.position, spawnPoint.rotation);
    }

    public void SetCurrentCorridor(GameObject corridor)
    {
        currentCorridor = corridor;
    }

    /// <summary>
    /// 배열 무작위 셔플
    /// </summary>
    /// <param name="array"></param>
    private void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }
}
