using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : InteractableObject, IInteractable
{
    [Header("개수 판정할 선반들 리스트")]
    public List<Shelf> shelfList;
    
    //정답 배열
    private List<int> answers = new List<int>();

    [Header("1차로 열릴 문")]
    public GameObject door ;

    [Header("문을 비춰줄 카메라")]
    [SerializeField] private CinemachineVirtualCamera doorCamera;

    [Header("문을 비춰줄 카메라가 찍을 레이어")]
    [SerializeField] LayerMask doorLayerMask;

    //메인 카메라
    private Camera mainCam;

    //이미 한번 실행했는지의 여부
    private bool isFinished;
    
    /// <summary>
    /// 정답을 미리 넣어주고 시작
    /// </summary>
    void Awake()
    {
        mainCam = Camera.main;
        answers = new List<int>() { 0, 4, 3, 1, 4 }; //0, 4, 3, 1, 4
    }

    public string GetInteractionText()
    {
        string str = "'E'키를 눌러 문 열기 시도";

        return str;
    }

    public string GetNameText()
    {
        return "제어 장치";
    }

    public void OnInteract()
    {
        TryOpenDoor();
    }

    /// <summary>
    /// Control Panel과 상호작용 했을때 정답개수와 일치하는지 검사하고 결과에 따라 행동을 수행하는 기능
    /// </summary>
    void TryOpenDoor()
    {
        if(!isFinished)
        {
            for (int i = 0; i < shelfList.Count; i++)
            {
                if (shelfList[i].activeCount != answers[i])
                {
                    FailToOpendoor();
                    return;
                }
            }
            SuccessToOpendoor();
        }
    }

    /// <summary>
    /// 정답을 맞추었을때 문 열기를 실행하는 기능
    /// </summary>
    void SuccessToOpendoor()
    {
        isFinished = true;
        door.transform.DOMove(door.transform.position + new Vector3(0, 4f, 0), 10f);
        StartCoroutine(ChangeCamPriority(doorCamera, 2000, 5f));
    }

    /// <summary>
    /// 정답을 맞추기 못했을 때 실패 디버프를 부여하는 기능 (스트레스 게이지)
    /// </summary>
    void FailToOpendoor()
    {

    }

    /// <summary>
    /// 카메라 연출을 위한 코루틴
    /// </summary>
    private IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        int originalPriority = cam.Priority;
        cam.Priority = priority;
        LayerMask layermask = mainCam.cullingMask;
        mainCam.cullingMask = doorLayerMask;

        yield return new WaitForSeconds(delay);

        cam.Priority = originalPriority;

        yield return new WaitForSeconds(2);
        mainCam.cullingMask = layermask;
    }

}
