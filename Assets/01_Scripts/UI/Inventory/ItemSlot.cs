using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    private Outline outline;

    public InventoryUI inventory;

    public int index;
    public bool selected;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        outline.enabled = selected;
    }

    /// <summary>
    /// �ش� ���� ����
    /// </summary>
   public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    /// <summary>
    /// �ش� ���� �ʱ�ȭ
    /// </summary>
   public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
