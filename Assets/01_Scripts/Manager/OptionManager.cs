using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public AudioSource audioSource;
    public SceneLoader sefse;
    public GameObject OptionPanel;
    public Slider bgmSlider; // Bgm 슬라이더
    public Slider sfxSlider; // SFX 슬라이더
    public Slider mouseSensitivitySlider; // 마우스 감도
    public GameObject continueButton; // 계속하기 버튼
    public TextMeshProUGUI volumeText; // 볼륨 값
    public TextMeshProUGUI sfxText; // 효과음 값
    public TextMeshProUGUI sensitivityText; // 마우스 감도 값

    private float mouseSensitivity = 1.0f; // 마우스 감도 기본값
    private float xRotation = 0f;
    private int currentStage; // 현재 스테이지
    private string sensitivityKey = "MouseSensitivity";
    private int MouseSensitivity;

    public void ToggleOptionPanel() // 옵션 패널 on/off 기능
    {
        if (OptionPanel != null)
        {
            bool wasActive = OptionPanel.activeSelf;
            OptionPanel.SetActive(!wasActive);

            if (wasActive) 
            {
                Savevalue();
            }
        }
    }


    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }


    void Start()
    {
        // 슬라이더 값 변경
        bgmSlider.onValueChanged.AddListener(delegate { SetBgmVolume((int)bgmSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolume((int)sfxSlider.value); });
        mouseSensitivitySlider.onValueChanged.AddListener(delegate { UpdateSmouseSensitivitySlider((int)mouseSensitivitySlider.value); }); ;

        // 처음으로 시작할때 볼륨
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        


        ApplySound(); // 볼륨적용

        mouseSensitivity = PlayerPrefs.GetInt(sensitivityKey, 1);
        mouseSensitivitySlider.value = mouseSensitivity; // 저장된 감도 불러오기

        //Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 고정

        OptionPanel.SetActive(false);

        int saveStage = PlayerPrefs.GetInt("Stage", 1); // 기본값 1로 고정

        ContinueButton();
    }

    public void SetVolume(int volume)
    {
        volumeText.text = volume.ToString();
        audioSource.volume = volume / 100f; // 0-100 -> 0-1 변환
        PlayerPrefs.SetInt("BgmVolume", volume);
    }

    public void SetSfx(int volume)
    {
        sfxText.text = volume.ToString();
        PlayerPrefs.SetInt("SfxVolume", volume);
    }

    public void SetSensitivity(int sensitivity)
    {
        sensitivityText.text = sensitivity.ToString();
        PlayerPrefs.SetInt(sensitivityKey, sensitivity);
    }

    public void SetBgmVolume(float volume) // BgmVolume 조절
    {
        bgmSlider.value = volume;
    }

    public void SetSfxVolume(float volume) //SfxVolume 조절
    {
        sfxSlider.value = volume;
    }

    public void ApplySound()
    {
        int bgm = PlayerPrefs.GetInt("BgmVolume", 100);
        int sfx = PlayerPrefs.GetInt("SfxVolume", 100);
        int sensitivity = PlayerPrefs.GetInt(sensitivityKey, 5);

        audioSource.volume = bgm / 100f;
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

    void SaveStageProgress(int stageNumber)
    {
        PlayerPrefs.SetInt("Stage", stageNumber);
        PlayerPrefs.Save();
    }

    public void LoadStage()
    {
        SceneLoader.Instance.LoadScene("Stage1" + currentStage); //스테이지 로드
    }

    void OnPlayRestart()
    {
        int lastStage = PlayerPrefs.GetInt("Stage", 1); // 마지막 스테이지에서 다시 시작
        SceneLoader.Instance.LoadScene("stage" + lastStage);
    }

    void ResetGame()
    {
        PlayerPrefs.SetInt("Stage", 1); // 처음 1스테이지부터로 초기화
        PlayerPrefs.Save();
        SceneLoader.Instance.LoadScene("Stage1");
    }

    void CleaerStage5()
    {
        PlayerPrefs.SetInt("Stage", 1); // 진행데이터 초기화
        PlayerPrefs.Save();

        GameObject continueButton = GameObject.Find("ContinueButton");
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
    }

    public void ContinueButton()
    {
        currentStage = PlayerPrefs.GetInt("Stage", 1);

        if (currentStage == 1 || currentStage > 1) // 스테이지 1이나 5를 클리어 했다면 버튼 비활성화
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }
}
