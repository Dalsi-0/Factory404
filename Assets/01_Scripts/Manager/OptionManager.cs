using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public Slider bgmSlider; // Bgm �����̴�
    public Slider sfxSlider; // SFX �����̴�
    public Toggle muteToggle; // ���Ұ�
    public Slider mouseSensitivitySlider; // ���콺 ����
    public Transform playerBody;

    private float mouseSensitivity = 1.0f; // ���콺 ���� �⺻��
    private float xRotation = 0f;

    public void ToggleOptionPanel() // �ɼ� �г� on/off ���
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
        Debug.Log("���� ����");
        Application.Quit();
    }
    
    
    void Start()
    {
        // �����̴� �� ����
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        muteToggle.onValueChanged.AddListener(ApplyVolume);

        // ó������ �����Ҷ� ����
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        ApplySound(); // ��������

        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        mouseSensitivitySlider.value = mouseSensitivity; // ����� ���� �ҷ�����

        mouseSensitivitySlider.onValueChanged.AddListener(UpdateSmouseSensitivitySlider); // ���� ����

        //Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ����

        OptionPanel.SetActive(false);
    }

    public void SetBgmVolume(float volume) // BgmVolume ����
    {
        bgmSlider.value = volume;
    }

    public void SetSfxVolume(float volume) //SfxVolume ����
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
