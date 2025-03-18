using System.Collections;
using TMPro;
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
        int speeker = chat[0][chat[0].Length-1] - '0';
        for (int i = 0; i < chat.Length; i++)
        {
            if (chat[i][chat[i].Length-1] != speeker)
            {
                speeker = chat[i][chat[i].Length - 1];

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
        for(int i = 0; i < str.Length - 1; i++)
        {
            chatText.text += str[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        chatText.text += "\n";
    }
}
