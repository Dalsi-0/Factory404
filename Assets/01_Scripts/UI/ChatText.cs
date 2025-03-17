using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChatText : MonoBehaviour
{
    private TextMeshProUGUI chatText;
    private ChatData chatData;
    [SerializeField] private string[] chat;

    public float typingSpeed;
    public float clearSpeed;

    private void Awake()
    {
        chatText = GetComponent<TextMeshProUGUI>();
        chatData = GameManager.Instance.chatData;
    }

    private void Start()
    {
        chatText.text = string.Empty;
        UpdateChatText(0);
    }

    /// <summary>
    /// id를 받아와서 출력할 chat을 초기화
    /// </summary>
    /// <param name="id"> id -> 지금 출력하고 싶은 chat의 id번호 </param>
    public void UpdateChatText(int id)
    {
        Chat c = chatData.chatDataList.Find(x => x.id == id);
        chat = c.content.Split("@");

        StartCoroutine(DisplayChat());
    }

    /// <summary>
    /// 대화내용을 화면에 출력, 최대 4문장까지 한 화면에 출력함
    /// </summary>
    /// <returns> UI의 ChatText에 대화 내용 출력 </returns>
    IEnumerator DisplayChat()
    {
        for (int i = 0; i < chat.Length; i++)
        {
            if (i % 4 == 0)
            {
                yield return new WaitForSeconds(clearSpeed);
                chatText.text = string.Empty;
            }
            yield return StartCoroutine(TypingText(chat[i]));
        }

        yield return new WaitForSeconds(clearSpeed);
        chatText.text = string.Empty;
    }

    /// <summary>
    /// chat 내용을 한글자씩 타이핑하는 효과를 주는 코루틴
    /// </summary>
    /// <param name="str"> 출력할 대화 내용 </param>
    /// <returns> typingSpeed마다 1글자 출력 </returns>
    IEnumerator TypingText(string str)
    {
        foreach (char c in str.ToCharArray())
        {
            chatText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
