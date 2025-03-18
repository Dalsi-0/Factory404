using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUITextStarter : MonoBehaviour
{
    public ChatText chatText;
    public int chatId;

    void Start()
    {
        chatText.UpdateChatText(chatId);
    }
}
