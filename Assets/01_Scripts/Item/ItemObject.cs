using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// ��ȣ�ۿ� ������ ������Ʈ�� �ٶ���� ��, ������Ʈ â�� ��� �ؽ�Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <returns> ������ �̸����� ������Ʈ ���� ���� </returns>
    public string GetInteractPrompt();

    /// <summary>
    /// ��ȣ�ۿ� ������ ������Ʈ�� �÷��̾ ��ȣ�ۿ����� �� ȣ��Ǵ� �Լ�
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
