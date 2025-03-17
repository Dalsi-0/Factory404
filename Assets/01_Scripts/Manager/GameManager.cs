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
    /// 스트레스로 보일 헛것 리스트로 저장
    /// </summary>
    public void SetGhostLightList()
    {
        ghostLight.Clear();

        ghostLight=GameObject.Find("GhostLightParent").GetComponentsInChildren<Light>().ToList();
        OnGhostLight(false);
    }

    /// <summary>
    /// 헛것들 Light 켜기
    /// </summary>
    public void OnGhostLight(bool set)
    {
        foreach (Light light in ghostLight)
        {
            light.gameObject.SetActive(set);
        }
    }
}
