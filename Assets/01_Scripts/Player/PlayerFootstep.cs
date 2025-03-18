using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClips = SoundManager.Instance.footSetpAudioClips;
    }

    /// <summary>
    /// 움직일 때 발소리
    /// </summary>
    public void OnFootStep()
    {
        SoundManager.Instance.PlaySFX(audioClips[Random.Range(0, audioClips.Length)].name, transform.position);
    }
}
