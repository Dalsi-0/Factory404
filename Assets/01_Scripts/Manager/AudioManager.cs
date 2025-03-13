using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider BgmSlider; // Bgm 슬라이더
    public Slider SfxSlider; // SFX 슬라이더
    public Toggle muteToggle; // 음소거

    void Start()
    {
        // 슬라이더 값 변경
        BgmSlider.onValueChanged.AddListener(SetBgmVolume);
        SfxSlider.onValueChanged.AddListener(SetSfxVolume);
        //muteToggle.onValueChanged.AddListener(SetMute);

        // 처음으로 시작할때 볼륨
        BgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f); 
        SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        ApplyVolume(); // 볼륨적용
    }

    public void SetBgmVolume(float volume) // BgmVolume 조절
    {
        BgmSlider.value = volume;
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }

    public void SetSfxVolume(float volume) //SfxVolume 조절
    {
        SfxSlider.value = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    private void ApplyVolume()
    {
        SoundManager.Instance.SetMute(muteToggle);     
    }
}
