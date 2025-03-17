using System.Collections.Generic;
using UnityEngine;

public class Shelf : InteractableObject, IInteractable
{
    [Header("Shelf ���� �ִ� ���ڵ� ���")]
    public List<GameObject> crates;

    public int activeCount;

    void Awake()
    {
        foreach (var crate in crates)
        {
            crate.SetActive(false);
        }
    }

    public string GetInteractionText()
    {
        string str = "'E'Ű�� ���� ȸ���ϱ�";

        return str;
    }

    public string GetNameText()
    {
        return "����";
    }

    public void OnInteract()
    {
        InterectWithShelf();
    }

    /// <summary>
    /// ���ݰ� ��ȣ�ۿ����� �� ���ڰ� ���� ��� ���ڸ� ���̴� ���(�ּҰ�������)
    /// </summary>
    public void InterectWithShelf()
    {
        if (crates.Count >= 0 && activeCount > 0)
        {
            crates[activeCount - 1].SetActive(false);
            activeCount--;
        }
    }
}
