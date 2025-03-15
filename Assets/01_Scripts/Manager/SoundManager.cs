﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips; // 오디오 클립 배열

    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private Dictionary<string, AudioClip> soundDict; // SFX와 BGM 저장용 Dictionary
    private AudioSource bgmPlayer; // BGM 재생용 AudioSource
    private Coroutine bgmFadeCoroutine;

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
        bgmPlayer.volume = bgmVolume; // 초기 볼륨 설정
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
    public void ChangeBGM(string newBgmName, float fadeDuration = 1.5f)
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

    public void SetMute(bool mute)
    {
        bgmVolume = 0;
        sfxVolume = 0;
    }
}
