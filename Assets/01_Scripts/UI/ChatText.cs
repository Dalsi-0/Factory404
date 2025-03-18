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
    /// id�� �޾ƿͼ� ����� chat�� �ʱ�ȭ
    /// </summary>
    /// <param name="id"> id -> ���� ����ϰ� ���� chat�� id��ȣ </param>
    public void UpdateChatText(int id)
    {
        Chat c = chatData.chatDataList.Find(x => x.id == id);
        chat = c.content.Split("@");

        StartCoroutine(DisplayChat());
    }

    /// <summary>
    /// ��ȭ������ ȭ�鿡 ���, �ִ� 4������� �� ȭ�鿡 �����
    /// </summary>
    /// <returns> UI�� ChatText�� ��ȭ ���� ��� </returns>
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
    /// chat ������ �ѱ��ھ� Ÿ�����ϴ� ȿ���� �ִ� �ڷ�ƾ
    /// </summary>
    /// <param name="str"> ����� ��ȭ ���� </param>
    /// <returns> typingSpeed���� 1���� ��� </returns>
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
