using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player _player;

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
}
