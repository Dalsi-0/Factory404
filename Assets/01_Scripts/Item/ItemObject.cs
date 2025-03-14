using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    /// <summary>
    /// UI�� ��� �̸��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns> �������� �̸� </returns>
    public string GetNameText();

    /// <summary>
    /// UI�� ��� ��ȣ�ۿ� ������ Ű�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns> ��ȣ�ۿ� ������ Ű�� ���� ���� </returns>
    public string GetInteractionText();

    /// <summary>
    /// ��ȣ�ۿ� ������ ������Ʈ�� �÷��̾ ��ȣ�ۿ����� �� ȣ��Ǵ� �Լ�
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
        string str = "'E'Ű�� ���� ������ ȹ��";

        return str;
    }

    public void OnInteract()
    {
        GameManager.Instance.Player.curItemData = data;

        GameManager.Instance.Player.addItem?.Invoke();

        Destroy(this.gameObject);
    }
}
