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
    /// 구글 스프레드 시트에서 데이터를 받아오는 함수
    /// </summary>
    /// <param name="list"> 구글 스프레드 시트의 셀 리스트 </param>
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

//  인스펙터 창에서 데이터를 불러올 수 있는 버튼을 생성 (커스텀 에디터)

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
    /// 커스터 에디터 버튼을 만들고, 버튼을 누르면 데이터를 불러옴
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기");

        if (GUILayout.Button("데이터 읽기(API 호출)"))
        {
            UpdateStats(UpdateMethodOne);
            data.chatDataList.Clear();
        }
    }

    /// <summary>
    /// 구글 스프레드 시트의 데이터를 읽는 함수
    /// </summary>
    /// <param name="callback"> 구글 스프레드 시트를 읽어온 후 이 데이터를 처리할 함수를 넣어야 함 </param>
    /// <param name="mergedcells"> 병합되어 있는 셀을 고려할 것 인지 (false면 고려x)</param>
    private void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedcells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.sheetURL,data.sheetName), callback, mergedcells);
    }

    /// <summary>
    /// 구글 시트의 내용을 data에 저장
    /// </summary>
    /// <param name="sheet"> 읽어온 구글 시트 </param>
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