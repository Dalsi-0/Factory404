using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    /// <summary>
    /// UI에 띄울 이름을 반환하는 함수
    /// </summary>
    /// <returns> 아이템의 이름 </returns>
    public string GetNameText();

    /// <summary>
    /// UI에 띄울 상호작용 가능한 키를 반환하는 함수
    /// </summary>
    /// <returns> 상호작용 가능한 키에 대한 설명 </returns>
    public string GetInteractionText();

    /// <summary>
    /// 상호작용 가능한 오브젝트와 플레이어가 상호작용했을 때 호출되는 함수
    /// </summary>
    public void OnInteract();
}

public class ItemObject : InteractableObject, IInteractable
{
    public ItemData data;

    public string GetNameText()
    {
        string str = data.itemName;

        return str;
    }

    public string GetInteractionText()
    {
        string str = "'E'키를 눌러 아이템 획득";

        return str;
    }

    public void OnInteract()
    {
        GameManager.Instance.Player.curItemData = data;

        GameManager.Instance.Player.addItem?.Invoke();

        Destroy(this.gameObject);
    }
}
