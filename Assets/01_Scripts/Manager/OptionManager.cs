using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public Slider bgmSlider; // Bgm �����̴�
    public Slider sfxSlider; // SFX �����̴�
    public Slider mouseSensitivitySlider; // ���콺 ����
    public Button continueButton; // ����ϱ� ��ư

    private float mouseSensitivity = 1.0f; // ���콺 ���� �⺻��
    private float xRotation = 0f;
    private int currentStage; // ���� ��������

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
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(false); // �ɼ� �г� ��Ȱ��ȭ
        }
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

        // ó������ �����Ҷ� ����
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);


        ApplySound(); // ��������

        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        mouseSensitivitySlider.value = mouseSensitivity; // ����� ���� �ҷ�����

        mouseSensitivitySlider.onValueChanged.AddListener(UpdateSmouseSensitivitySlider); // ���� ����

        //Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ����

        OptionPanel.SetActive(false);

        int saveStage = PlayerPrefs.GetInt("Stage", 1); // �⺻�� 1�� ����
        LoadStage(saveStage);
    }

    public void SetBgmVolume(float volume) // BgmVolume ����
    {
        bgmSlider.value = volume;
    }

    public void SetSfxVolume(float volume) //SfxVolume ����
    {
        sfxSlider.value = volume;
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

    void SaveStageProgress(int stageNumber)
    {
        PlayerPrefs.SetInt("Stage", stageNumber);
        PlayerPrefs.Save();
    }

    public void LoadStage(int stageNumber)
    {
        SceneLoader.Instance.LoadScene("Stage1" + stageNumber); //�������� �ε�
    }

    void OnPlayRestart()
    {
        int lastStage = PlayerPrefs.GetInt("Stage", 1); // ������ ������������ �ٽ� ����
        SceneLoader.Instance.LoadScene("stage" + lastStage);
    }

    void ResetGame()
    {
        PlayerPrefs.SetInt("Stage", 1); // ó�� 1�����������ͷ� �ʱ�ȭ
        PlayerPrefs.Save();
        SceneLoader.Instance.LoadScene("Stage1");
    }

    void CleaerStage5()
    {
        PlayerPrefs.SetInt("Stage", 1); // ���൥���� �ʱ�ȭ
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

        if (currentStage == 1 || currentStage > 1) // �������� 1�̳� 5�� Ŭ���� �ߴٸ� ��ư ��Ȱ��ȭ
        {
            continueButton.gameObject.SetActive(false);
        }
        else
        {
            continueButton.gameObject.SetActive(true);
        }
    }
}
