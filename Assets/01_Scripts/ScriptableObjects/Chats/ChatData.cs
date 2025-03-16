using GoogleSheetsToUnity;
using System;
using System.Collections.Generic;
using System.Data;
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
    public List<Chat> chatData = new List<Chat>();

    internal void UpdateStats(List<GSTU_Cell> list, int chatId)
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
        chatData.Add(new Chat(id, content));
    }
}


[CustomEditor(typeof(ChatData))]
public class ChatDataReaderEditor : Editor
{
    ChatData data;

    private void OnEnable()
    {
        data = (ChatData)target;    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기");

        if (GUILayout.Button("데이터 읽기(API 호출)"))
        {
            UpdateStats(UpdateMethodOne);
            data.chatData.Clear();
        }
    }

    private void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedcells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.sheetURL,data.sheetName), callback, mergedcells);
    }
    private void UpdateMethodOne(GstuSpreadSheet sheet)
    {
        for (int i = data.startRowIndex; i <= data.endRowIndex; ++i)
        {
            data.UpdateStats(sheet.rows[i], i);
        }

        EditorUtility.SetDirty(target);
    }
}
