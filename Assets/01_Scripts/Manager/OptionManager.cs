using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public Slider bgmSlider; // Bgm 슬라이더
    public Slider sfxSlider; // SFX 슬라이더
    public Toggle muteToggle; // 음소거
    public Slider mouseSensitivitySlider; // 마우스 감도
    public Transform playerBody;

    private float mouseSensitivity = 1.0f; // 마우스 감도 기본값
    private float xRotation = 0f;

    public void ToggleOptionPanel() // 옵션 패널 on/off 기능
    {
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(!OptionPanel.activeSelf);

            if (!OptionPanel.activeSelf)
            {
                Savevalue();
            }
        }
    }

    public void CloseOptionpaenl()
    {

    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
    
    
    void Start()
    {
        // 슬라이더 값 변경
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        muteToggle.onValueChanged.AddListener(ApplyVolume);

        // 처음으로 시작할때 볼륨
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        ApplySound(); // 볼륨적용

        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        mouseSensitivitySlider.value = mouseSensitivity; // 저장된 감도 불러오기

        mouseSensitivitySlider.onValueChanged.AddListener(UpdateSmouseSensitivitySlider); // 감도 조절

        //Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 고정

        OptionPanel.SetActive(false);
    }

    public void SetBgmVolume(float volume) // BgmVolume 조절
    {
        bgmSlider.value = volume;
    }

    public void SetSfxVolume(float volume) //SfxVolume 조절
    {
        sfxSlider.value = volume;
    }

    private void ApplyVolume(bool mute)
    {
        SoundManager.Instance.SetMute(muteToggle);
    }

    public void ApplySound()
    {
        //OptionManager.Instance.
    }


    private void UpdateSmouseSensitivitySlider(float value)
    {
        mouseSensitivity = value;
    }
    public float GetSensitivity()
    {
        return mouseSensitivity;
    }

    public void Savevalue()
    {
        PlayerPrefs.SetFloat("BgmVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        SceneLoader.Instance.LoadScene("Stage1");
    }
}
