using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider BgmSlider; // Bgm �����̴�
    public Slider SfxSlider; // SFX �����̴�
    public Toggle muteToggle; // ���Ұ�

    void Start()
    {
        // �����̴� �� ����
        BgmSlider.onValueChanged.AddListener(SetBgmVolume);
        SfxSlider.onValueChanged.AddListener(SetSfxVolume);
        //muteToggle.onValueChanged.AddListener(SetMute);

        // ó������ �����Ҷ� ����
        BgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f); 
        SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        ApplyVolume(); // ��������
    }

    public void SetBgmVolume(float volume) // BgmVolume ����
    {
        BgmSlider.value = volume;
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }

    public void SetSfxVolume(float volume) //SfxVolume ����
    {
        SfxSlider.value = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    private void ApplyVolume()
    {
        SoundManager.Instance.SetMute(muteToggle);     
    }
}
