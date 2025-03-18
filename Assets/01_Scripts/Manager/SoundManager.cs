using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("사용할 모든 오디오 클립")]
    [SerializeField] private AudioClip[] audioClips; // 오디오 클립 배열

    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private Dictionary<string, AudioClip> soundDict; // SFX와 BGM 저장용 Dictionary
    private AudioSource bgmPlayer; // BGM 재생용 AudioSource
    private Coroutine bgmFadeCoroutine;

    [Header("랜덤 스산한 효과음")]
    [SerializeField] private AudioClip[] randomSFXAudioClipName; // 오디오 클립 배열
    [SerializeField] private float randomSFXInterval = 4f;
    private Coroutine randomSFXCoroutine;

    [Header("발소리")]
    public AudioClip[] footSetpAudioClips; // 발소리4개 클립 배열

    // 스트레스 반복 효과음
    [SerializeField] private float stressSFXInterval = 5f; // 효과음 반복 간격
    [SerializeField] private float stressBGMInterval = 1f; // 효과음 반복 간격
    private Coroutine stressSFXSoundCoroutine;
    private Coroutine stressBGMSoundCoroutine;

    private void Awake()
    {
        if (_instance != null && _instance != (this as SoundManager))
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitSoundManager();
    }

    /// <summary>
    /// 오디오 클립을 Dictionary에 저장하고 BGM 플레이어를 설정
    /// </summary>
    private void InitSoundManager()
    {
        // Dictionary 초기화
        soundDict = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }

        // BGM 플레이어 초기화
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume; 

        // 3D 공간 효과 제거
        bgmPlayer.spatialBlend = 0f; 
        bgmPlayer.rolloffMode = AudioRolloffMode.Linear; 
        bgmPlayer.dopplerLevel = 0f; 
        bgmPlayer.spread = 0f; 
    }

    /// <summary>
    /// 특정 위치에서 효과음을 재생
    /// </summary>
    /// <param name="soundName">재생할 효과음의 이름</param>
    /// <param name="position">효과음이 재생될 위치</param>
    public void PlaySFX(string soundName, Vector3 position)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            AudioSource.PlayClipAtPoint(clip, position, sfxVolume);
        }
        else
        {
            Debug.LogWarning($"SFX {soundName} not found");
        }
    }

    /// <summary>
    /// 배경 음악(BGM)을 즉시 재생
    /// </summary>
    /// <param name="bgmName">재생할 BGM의 이름</param>
    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (bgmPlayer.clip != clip)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.volume = bgmVolume;
                bgmPlayer.Play();
            }
        }
        else
        {
            Debug.LogWarning($"BGM {bgmName} not found");
        }
    }

    /// <summary>
    /// 배경 음악(BGM)을 서서히 변경
    /// </summary>
    /// <param name="newBgmName">변경할 BGM의 이름</param>
    /// <param name="fadeDuration">페이드 아웃,인 시간</param>
    public void ChangeBGM(string newBgmName, float fadeDuration = 1f)
    {
        if (soundDict.TryGetValue(newBgmName, out var newClip))
        {
            if (bgmPlayer.clip == newClip) return;

            if (bgmFadeCoroutine != null)
                StopCoroutine(bgmFadeCoroutine);

            bgmFadeCoroutine = StartCoroutine(FadeOutAndChangeBGM(newClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"BGM '{newBgmName}' not found");
        }
    }

    /// <summary>
    /// 배경 음악(BGM)을 서서히 페이드 아웃합니다.
    /// </summary>
    /// <param name="fadeDuration">페이드 아웃 지속 시간</param>
    public void FadeOutBGM(float fadeDuration = 1.5f)
    {
        if (bgmFadeCoroutine != null)
            StopCoroutine(bgmFadeCoroutine);

        bgmFadeCoroutine = StartCoroutine(FadeOutBGMCoroutine(fadeDuration));
    }

    /// <summary>
    /// playerTransform 주변에서 일정 시간 간격으로 랜덤한 효과음을 재생하는 기능을 시작합니다.
    /// </summary>
    public void PlayRandomSFXPeriodically(Transform playerTransform)
    {
        if (randomSFXCoroutine != null)
        {
            StopCoroutine(randomSFXCoroutine);
        }
        randomSFXCoroutine = StartCoroutine(RandomSFXCoroutine(playerTransform));
    }

    /// <summary>
    /// 랜덤 공포 효과음 멈추는 기능
    /// </summary>
    public void StopPlayRandomSFX()
    {
        if (randomSFXCoroutine != null)
        {
            StopCoroutine(randomSFXCoroutine);
        }
    }

    /// <summary>
    /// playerTransform 주변의 랜덤한 위치에서 일정한 간격으로 랜덤한 효과음을 재생하는 코루틴입니다.
    /// </summary>
    private IEnumerator RandomSFXCoroutine(Transform playerTransform)
    {
        while (true)
        {
            float randomDelay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(randomSFXInterval + randomDelay);

            if (randomSFXAudioClipName.Length > 0)
            {
                string randomSound = randomSFXAudioClipName[Random.Range(0, randomSFXAudioClipName.Length)].name;
                Vector3 randomPosition = playerTransform.position + Random.insideUnitSphere * 4f;
                randomPosition.y = playerTransform.position.y;
                PlaySFX(randomSound, randomPosition);
            }
        }
    }

    /// <summary>
    /// 현재 재생 중인 BGM을 서서히 페이드 아웃
    /// </summary>
    /// <param name="duration">페이드 아웃 지속 시간</param>
    private IEnumerator FadeOutBGMCoroutine(float duration)
    {
        float startVolume = bgmPlayer.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bgmPlayer.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            yield return null;
        }

        bgmPlayer.Stop();
        bgmPlayer.clip = null;
    }

    /// <summary>
    /// BGM을 서서히 페이드 아웃한 후 새로운 BGM을 페이드 인하여 변경
    /// </summary>
    /// <param name="newClip">변경할 새로운 BGM 오디오 클립</param>
    /// <param name="duration">페이드 아웃 및 페이드 인에 걸리는 시간</param>
    private IEnumerator FadeOutAndChangeBGM(AudioClip newClip, float duration)
    {
        float startVolume = bgmPlayer.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bgmPlayer.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            yield return null;
        }

        bgmPlayer.Stop();
        bgmPlayer.clip = newClip;
        bgmPlayer.Play();

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bgmPlayer.volume = Mathf.Lerp(0f, bgmVolume, elapsedTime / duration);
            yield return null;
        }
    }

    /// <summary>
    /// BGM 볼륨을 변경
    /// </summary>
    /// <param name="volume">설정할 볼륨 값 0~1</param>
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmPlayer.volume = bgmVolume;
    }

    /// <summary>
    /// 효과음 볼륨을 변경
    /// </summary>
    /// <param name="volume">설정할 볼륨 값 0~1</param>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// 모든 소리 0
    /// </summary>
    /// <param name="mute"></param>
    public void SetMute(bool mute)
    {
        bgmVolume = 0;
        sfxVolume = 0;
    }

    public void StartStressSoundCoroutine()
    {
        stressSFXSoundCoroutine = StartCoroutine(StressSFXSoundRoutine());
        stressBGMSoundCoroutine = StartCoroutine(StressBGMSoundRoutine());
    }

    public void StopStressSoundCoroutine()
    {
        StopCoroutine(stressSFXSoundCoroutine);
        StopCoroutine(stressBGMSoundCoroutine);
    }

    /// <summary>
    /// 스트레스 수준에 따라 효과음을 재생하는 코루틴
    /// </summary>
    private IEnumerator StressSFXSoundRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.Player != null)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }

        Player player = GameManager.Instance.Player;
        PlayerStress playerStress = GameManager.Instance.Player.stress;

        while (true)
        {
            float stressValue = playerStress.GetStressValue();
            yield return new WaitForSeconds(stressSFXInterval); // 일정 주기마다 실행

            if (stressValue >= 70f)
            {
                ChangeBGM("BGM_Stress70");
                PlaySFX("SFX_Beat", player.transform.position);
                yield return new WaitForSeconds(Random.Range(1, 3));
                PlaySFX("SFX_Breathing", player.transform.position);
            }
            else if (stressValue >= 30f)
            {
                ChangeBGM("BGM_Stress30");
                PlaySFX("SFX_Beat", player.transform.position);
            }
            else if (stressValue < 30)
            {
                ChangeBGM("BGM_Factory");
            }
        }
    }

    /// <summary>
    /// 스트레스 수준에 따라 배경음을 재생하는 코루틴
    /// </summary>
    private IEnumerator StressBGMSoundRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.Player != null)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }

        PlayerStress playerStress = GameManager.Instance.Player.stress;

        while (true)
        {
            float stressValue = playerStress.GetStressValue();
            yield return new WaitForSeconds(stressBGMInterval); // 일정 주기마다 실행

            if (stressValue >= 70f)
            {
                ChangeBGM("BGM_Stress70");
            }
            else if (stressValue >= 30f)
            {
                ChangeBGM("BGM_Stress30");
            }
            else if (stressValue < 30)
            {
                ChangeBGM("BGM_Factory");
            }
        }
    }
}
