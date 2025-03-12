using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// 상호작용 가능한 오브젝트를 바라봤을 때, 프롬프트 창에 띄울 텍스트를 전달하는 함수
    /// </summary>
    /// <returns> 아이템 이름같은 오브젝트 정보 전달 </returns>
    public string GetInteractPrompt();

    /// <summary>
    /// 상호작용 가능한 오브젝트와 플레이어가 상호작용했을 때 호출되는 함수
    /// </summary>
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.itemName}";

        return str;
    }

    public void OnInteract()
    {
        //CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(this.gameObject);
    }
}
