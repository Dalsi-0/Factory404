using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : Singleton<OptionManager>
{
    public GameObject OptionPanel;
    public Slider bgmSlider; // Bgm 슬라이더
    public Slider sfxSlider; // SFX 슬라이더
    public Slider mouseSensitivitySlider; // 마우스 감도
    public GameObject continueButton; // 계속하기 버튼
    public TextMeshProUGUI bgmSliderValueText;
    public TextMeshProUGUI sfxSliderValueText;
    public TextMeshProUGUI mouseSensitivitySliderValueText;
    public GameObject optionExitButton;

    public float mouseSensitivity = 0.5f; // 마우스 감도 기본값
    private int currentStage; // 현재 스테이지

    void Start()
    {
        // 슬라이더 값 변경
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);

        // 처음으로 시작할때 볼륨
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);

        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);

        mouseSensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivitySlider); // 감도 조절
        mouseSensitivitySlider.value = mouseSensitivity; // 저장된 감도 불러오기

        OptionPanel.SetActive(false);

        int saveStage = PlayerPrefs.GetInt("Stage", 1); // 기본값 1로 고정

        ContinueButton();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player player = FindObjectOfType<Player>();

        if(player != null)
        {
            PlayerControlloer controller = player.GetComponent<PlayerControlloer>();

            if(controller != null)
            {
                controller.option += ToggleOptionPanel;
            }
        }


        if (scene.name.Substring(0, 5) == "Stage")
        {
            int StageNum = int.Parse(scene.name.Substring(5));

            PlayerPrefs.SetInt("Stage", StageNum);
            PlayerPrefs.Save();

            optionExitButton.SetActive(false);
        }
        else
        {
            optionExitButton.SetActive(true);
        }
    }

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
        Application.Quit();
    }

    public void SetBgmVolume(float volume) // BgmVolume 조절
    {
        bgmSlider.value = volume;
        bgmSliderValueText.text = Mathf.FloorToInt(bgmSlider.value * 100f).ToString();
        SoundManager.Instance.SetBGMVolume(bgmSlider.value);
    }

    public void SetSfxVolume(float volume) //SfxVolume 조절
    {
        sfxSlider.value = volume;
        sfxSliderValueText.text = Mathf.FloorToInt(sfxSlider.value * 100f).ToString();
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    private void UpdateMouseSensitivitySlider(float value)
    {
        mouseSensitivity = value;
        mouseSensitivitySliderValueText.text = Mathf.FloorToInt(mouseSensitivitySlider.value * 100f).ToString();
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
        SoundManager.Instance.StopPlayRandomSFX();
        SceneLoader.Instance.LoadScene("Stage1");
    }

    void SaveStageProgress(int stageNumber)
    {
        PlayerPrefs.SetInt("Stage", stageNumber);
        PlayerPrefs.Save();
    }

    public void LoadStage()
    {
        SceneLoader.Instance.LoadScene("Stage" + currentStage); //스테이지 로드
    }

    void OnPlayRestart()
    {
        int lastStage = PlayerPrefs.GetInt("Stage", 1); // 마지막 스테이지에서 다시 시작
        SceneLoader.Instance.LoadScene("stage" + lastStage);
    }

    public void ResetGame()
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
        if(continueButton != null)
        {
            if (currentStage == 1) // 스테이지 1이나 5를 클리어 했다면 버튼 비활성화
            {
                continueButton.SetActive(false);
            }
            else
            {
                continueButton.SetActive(true);
            }
        }
    }
}
