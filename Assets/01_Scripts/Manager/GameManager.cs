using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

public class GameManager : Singleton<GameManager>
{
    private Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private List<Light> ghostLight = new List<Light>();

    public ChatData chatData;

    public SpriteAtlas iconSpriteAtlas;

    private void Awake()
    {
        if (_instance != null && _instance != (this as GameManager))
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    /// 스트레스로 보일 헛것 리스트로 저장 및 초기화
    /// </summary>
    public void SetGhostLightList()
    {
        ghostLight.Clear();

        ghostLight=GameObject.Find("GhostLightParent").GetComponentsInChildren<Light>().ToList();
        OnGhostLight(false);
    }

    /// <summary>
    /// 헛것들 Light 켜기/끄기
    /// </summary>
    public void OnGhostLight(bool set)
    {
        foreach (Light light in ghostLight)
        {
            light.gameObject.SetActive(set);
        }
    }
}
