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
    }

    /// <summary>
    /// ������ �� �߼Ҹ�
    /// </summary>
    public void OnFootStep()
    {
        Debug.Log("�߼Ҹ�");
        //SoundManager.Instance.PlaySFX(audioClips[Random.Range(0, audioClips.Length)].name, transform.position);
    }
}
