using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player _player;


    private List<Light> ghostLight = new List<Light>();
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public ChatData chatData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    /// ��Ʈ������ ���� ��� ����Ʈ�� ����
    /// </summary>
    public void SetGhostLightList()
    {
        ghostLight.Clear();

        ghostLight=GameObject.Find("GhostLightParent").GetComponentsInChildren<Light>().ToList();
        OnGhostLight(false);
    }

    /// <summary>
    /// ��͵� Light �ѱ�
    /// </summary>
    public void OnGhostLight(bool set)
    {
        foreach (Light light in ghostLight)
        {
            light.gameObject.SetActive(set);
        }
    }
}
