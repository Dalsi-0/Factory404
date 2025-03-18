using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class SceneStartHelper : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    public ChatText ChatText;
    public int chatId;

    void Start()
    {
        playableDirector.stopped += OnTimelineEnd;
        StartCoroutine(StartScene());
    }

    /// <summary>
    /// 씬 시작하고 작동되어야할 타이밍
    /// </summary>
    /// <returns></returns>
    IEnumerator StartScene()
    {
        // 문 닫히는 소리 작동
        SoundManager.Instance.PlaySFX("SFX_Closedoor", GameManager.Instance.Player.transform.position);

        yield return new WaitForEndOfFrame(); // 한 프레임 안전장치
        // 시네머신 작동
        playableDirector.Play();
        // Playerinput막고
        GameManager.Instance.Player.playerInput.enabled = false;
        // 상호작용 오브젝트 막고 
        GameManager.Instance.Player.playerInteraction.SetisRayInteractionActive(false);
        // 팩토리 브금 실행
        SoundManager.Instance.ChangeBGM("BGM_Factory");
    }

    /// <summary>
    /// 가이드 타임라인 종료되고 실행될 함수
    /// </summary>
    IEnumerator EndGuideTimeline()
    {
        yield return new WaitForSeconds(2.5f); // 카메라 돌아가는 시간
        // Playuerinput 풀고
        GameManager.Instance.Player.playerInput.enabled = true;
        // 상호작용 오브젝트 풀고
        GameManager.Instance.Player.playerInteraction.SetisRayInteractionActive(true);
        // 랜덤 공포 효과음 작동
        SoundManager.Instance.PlayRandomSFXPeriodically(GameManager.Instance.Player.transform);

        SoundManager.Instance.StartStressSoundCoroutine();
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= OnTimelineEnd;
        SoundManager.Instance.StopStressSoundCoroutine();
        SoundManager.Instance.StopPlayRandomSFX();
    }

    /// <summary>
    /// 타임라인 종료시
    /// </summary>
    /// <param name="director"></param>
    private void OnTimelineEnd(PlayableDirector director)
    {
        playableDirector.transform.parent.gameObject.SetActive(false);
        ChatText.UpdateChatText(chatId);
        StartCoroutine(EndGuideTimeline());
    }
}
