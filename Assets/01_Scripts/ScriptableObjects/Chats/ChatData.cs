using GoogleSheetsToUnity;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public struct Chat
{
    public int id;
    public string content;

    public Chat(int id, string content)
    {
        this.id = id;
        this.content = content;
    }
}

[CreateAssetMenu(fileName = "Chat", menuName = "Scriptable Object/New ChatData")]
public class ChatData : DataReaderBase
{
    public List<Chat> chatDataList = new List<Chat>();

    /// <summary>
    /// ���� �������� ��Ʈ���� �����͸� �޾ƿ��� �Լ�
    /// </summary>
    /// <param name="list"> ���� �������� ��Ʈ�� �� ����Ʈ </param>
    internal void UpdateStats(List<GSTU_Cell> list)
    {
        int id = 0;
        string content = "";

        for(int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "id":
                    id = int.Parse(list[i].value);
                    break;
                case "content":
                    content = list[i].value;
                    break;
            }
        }
        chatDataList.Add(new Chat(id, content));
    }
}

//  �ν����� â���� �����͸� �ҷ��� �� �ִ� ��ư�� ���� (Ŀ���� ������)

#if UNITY_EDITOR
[CustomEditor(typeof(ChatData))]
public class ChatDataReaderEditor : Editor
{
    ChatData data;

    private void OnEnable()
    {
        data = (ChatData)target;    
    }

    /// <summary>
    /// Ŀ���� ������ ��ư�� �����, ��ư�� ������ �����͸� �ҷ���
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n�������� ��Ʈ �о����");

        if (GUILayout.Button("������ �б�(API ȣ��)"))
        {
            UpdateStats(UpdateMethodOne);
            data.chatDataList.Clear();
        }
    }

    /// <summary>
    /// ���� �������� ��Ʈ�� �����͸� �д� �Լ�
    /// </summary>
    /// <param name="callback"> ���� �������� ��Ʈ�� �о�� �� �� �����͸� ó���� �Լ��� �־�� �� </param>
    /// <param name="mergedcells"> ���յǾ� �ִ� ���� ����� �� ���� (false�� ���x)</param>
    private void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedcells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.sheetURL,data.sheetName), callback, mergedcells);
    }

    /// <summary>
    /// ���� ��Ʈ�� ������ data�� ����
    /// </summary>
    /// <param name="sheet"> �о�� ���� ��Ʈ </param>
    private void UpdateMethodOne(GstuSpreadSheet sheet)
    {
        for (int i = data.startRowIndex; i <= data.endRowIndex; ++i)
        {
            data.UpdateStats(sheet.rows[i]);
        }

        EditorUtility.SetDirty(target);
    }
}
#endif