using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SceneThumbnail
{
    public string sceneName;
    public Sprite thumbnail;
}

public class SceneLoader : Singleton<SceneLoader>
{
    [Header("UI 참조")]
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Image loadingBar;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private TextMeshProUGUI loadingText;

    [Header("씬 썸네일")]
    [SerializeField] private List<SceneThumbnail> sceneThumbnails;
    private Dictionary<string, Sprite> thumbnailDict = new Dictionary<string, Sprite>();

    private string nextScene;

    private void Awake()
    {
        if (_instance != null && _instance != (this as SceneLoader))
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitializeThumbnails();
    }

    /// <summary>
    /// 씬별 썸네일 정보를 Dictionary에 저장 및 참조 객체 상태 초기화
    /// </summary>
    private void InitializeThumbnails()
    {
        loadingCanvas.SetActive(false);
        fadeCanvasGroup.gameObject.SetActive(false);

        foreach (var item in sceneThumbnails)
        {
            thumbnailDict[item.sceneName] = item.thumbnail;
        }
    }


    /// <summary>
    /// 현재씬 이름 가져오기
    /// </summary>
    /// <returns></returns>
    public string GetCurrentScene()
    {
        return nextScene;
    }

    /// <summary>
    /// 씬을 비동기적으로 로드하며 먼저 로딩 씬을 거쳐서 진행
    /// </summary>
    /// <param name="sceneName">로드할 씬의 이름</param>
    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        RenderSettings.fog = false;

        if (sceneName == "StageEnd01" || sceneName == "StageEnd02")
        {
            Cursor.lockState = CursorLockMode.None;
            SoundManager.Instance.ChangeBGM("BGM_Memories");
        }
        else if(sceneName == "StartScene")
        {
            SoundManager.Instance.ChangeBGM("BGM_StartScene");
            RenderSettings.fog = true;
        }


        StartCoroutine(LoadSceneCoroutine());
    }

    /// <summary>
    /// 씬 로딩
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine()
    {
        yield return StartCoroutine(FadeToBlack());

        yield return SceneManager.LoadSceneAsync("LoadingScene");

        fadeCanvasGroup.gameObject.SetActive(false);
        loadingCanvas.SetActive(true);
        SetThumbnail();

        yield return new WaitForSeconds(1f); // 연출상 잠깐 대기

        StartCoroutine(AnimateLoadingText());

        // 본 씬 로드
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        loadingCanvas.SetActive(false);
        yield return StartCoroutine(FadeToClear());
    }

    /// <summary>
    /// 로딩 씬에서 적절한 배경 썸네일을 설정
    /// </summary>
    private void SetThumbnail()
    {
        if (thumbnailDict.TryGetValue(nextScene, out Sprite thumbnail))
        {
            backgroundImage.sprite = thumbnail;
        }
        else
        {
            backgroundImage.sprite = null;
        }
    }

    /// <summary>
    /// 화면 검게 변하는 페이드 아웃
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeToBlack()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
    }

    /// <summary>
    /// 화면 밝아지는 페이드 인
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeToClear()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    /// <summary>
    /// "Loading..." 뒤 점이 변하는 애니메이션 효과
    /// </summary>
    private IEnumerator AnimateLoadingText()
    {
        string baseText = "Loading";
        string[] dots = { ".", "..", "...", " " };

        int index = 0;
        while (loadingCanvas.activeSelf)
        {
            loadingText.text = baseText + dots[index];
            index = (index + 1) % dots.Length;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
