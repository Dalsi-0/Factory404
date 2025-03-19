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
        // 소리
        SoundManager.Instance.PlayBGM("BGM_StartScene");
        SoundManager.Instance.PlayRandomSFXPeriodically(introAgent);
        StartCoroutine(AutoFootStepSound());

        // 통로의 끝을 안보이게 하기 위한 검은 안개 세팅
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0,0,0);
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.2f;

        currentCorridor = null;
        SpawnerNewCorridors(originPos);
        introAgent.position = originPos.position;
        ShuffleArray(prefabsCorridors);
    }

    /// <summary>
    /// 자동 발걸음 소리 재생
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoFootStepSound()
    {
        Transform camera = Camera.main.transform;

        AudioClip[] footStep = SoundManager.Instance.footSetpAudioClips;

        while (true)
        {
            SoundManager.Instance.PlaySFX(footStep[Random.Range(0, 4)].name, camera.position);

            yield return new WaitForSeconds(.75f);
        }
    }

    /// <summary>
    /// 새로운 복도 생성 및 이전 복도는 삭제
    /// </summary>
    /// <param name="spawnPoint"></param>
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
