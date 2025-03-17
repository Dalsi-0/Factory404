using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : InteractableObject, IInteractable
{
    [Header("���� ������ ���ݵ� ����Ʈ")]
    public List<Shelf> shelfList;
    
    //���� �迭
    private List<int> answers = new List<int>();

    [Header("1���� ���� ��")]
    public GameObject door ;

    [Header("���� ������ ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera doorCamera;

    [Header("���� ������ ī�޶� ���� ���̾�")]
    [SerializeField] LayerMask doorLayerMask;

    //���� ī�޶�
    private Camera mainCam;

    //�̹� �ѹ� �����ߴ����� ����
    private bool isFinished;
    
    /// <summary>
    /// ������ �̸� �־��ְ� ����
    /// </summary>
    void Awake()
    {
        mainCam = Camera.main;
        answers = new List<int>() { 0, 4, 3, 1, 4 }; //0, 4, 3, 1, 4
    }

    public string GetInteractionText()
    {
        string str = "'E'Ű�� ���� �� ���� �õ�";

        return str;
    }

    public string GetNameText()
    {
        return "���� ��ġ";
    }

    public void OnInteract()
    {
        TryOpenDoor();
    }

    /// <summary>
    /// Control Panel�� ��ȣ�ۿ� ������ ���䰳���� ��ġ�ϴ��� �˻��ϰ� ����� ���� �ൿ�� �����ϴ� ���
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
    /// ������ ���߾����� �� ���⸦ �����ϴ� ���
    /// </summary>
    void SuccessToOpendoor()
    {
        isFinished = true;
        door.transform.DOMove(door.transform.position + new Vector3(0, 4f, 0), 10f);
        StartCoroutine(ChangeCamPriority(doorCamera, 2000, 5f));
    }

    /// <summary>
    /// ������ ���߱� ������ �� ���� ������� �ο��ϴ� ��� (��Ʈ���� ������)
    /// </summary>
    void FailToOpendoor()
    {

    }

    /// <summary>
    /// ī�޶� ������ ���� �ڷ�ƾ
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
