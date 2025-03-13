using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    public Button detailButton;
    public GameObject detailWindow;
    public DetailUI detailUI;

    [Header("Select Item Info")]
    public Image selectedItemIcon;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    private ItemData selectedItem;

    private void Start()
    {
        inventoryWindow.SetActive(true);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
        UpdateSlots();
    }

    /// <summary>
    /// ���õ� ���� �ʱ�ȭ
    /// </summary>
    private void ClearSelectedItemWindow()
    {
        selectedItemIcon.gameObject.SetActive(false);
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        detailButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// �κ��丮 ��� �Լ�
    /// </summary>
    public void InventoryToggle()
    {
        if (isOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool isOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    /// <summary>
    /// �������� ȹ���ϴ� �Լ�
    /// </summary>
    /// <param name="data"> -> ȹ���� ������ </param>
    public void AddItem(ItemData data)
    {
        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = data;
            UpdateSlots();
        }
        else
        {
            //  �κ��丮�� ��á�� �� ó��. ��Ż�� �����̶� �ʿ����� �����غ� �ʿ� ����.
        }
    }

    /// <summary>
    /// �󽽷��� �������� �Լ�
    /// </summary>
    /// <returns> �󽽷��� ������ �ش� ���� ��ȯ, �󽽷��� ������ null ��ȯ </returns>
    private ItemSlot GetEmptySlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    /// <summary>
    /// ��� ������ ���¸� ������Ʈ ���ִ� �Լ�
    /// </summary>
    private void UpdateSlots()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    /// <summary>
    /// ������ �����ϰ� ���õ� ������ �������� ������ ǥ������
    /// </summary>
    /// <param name="index"> -> ������ ������ ������ �ּ� </param>
    public void SelectItem(int index)
    {
        if (slots[index].item == null)
        {
            return;
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == slots[index])
            {
                slots[i].selected = true;
            }
            else
            {
                slots[i].selected = false;
            }
        }

        selectedItem = slots[index].item;

        selectedItemIcon.gameObject.SetActive(true);
        detailButton.gameObject.SetActive(true);
        selectedItemIcon.sprite = selectedItem.icon;
        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
    }

    /// <summary>
    /// �κ��丮�� �������� �����ϴ� �� ã�� �Լ�
    /// </summary>
    /// <param name="name"> -> ã�� ���� �������� �̸� </param>
    /// <returns> �������� �����ϸ� �� �������� ������ ��ȯ, �������� ������ null�� ��ȯ </returns>
    public ItemData FindItem(string name)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemName == name)
            {
                return slots[i].item;
            }
        }
        return null;
    }

    /// <summary>
    /// ��ư�� Ŭ���ϸ� ������â�� selectedItem ������ �ѱ��, �κ��丮â�� ������ ������ â�� ������
    /// </summary>
    public void OnClickDetailButton()
    {
        detailUI.GetItemInfo(selectedItem);
        detailWindow.SetActive(true);
        InventoryToggle();
    }
}
