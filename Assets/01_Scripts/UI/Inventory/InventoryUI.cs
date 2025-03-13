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
    /// 선택된 슬롯 초기화
    /// </summary>
    private void ClearSelectedItemWindow()
    {
        selectedItemIcon.gameObject.SetActive(false);
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        detailButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 인벤토리 토글 함수
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
    /// 아이템을 획득하는 함수
    /// </summary>
    /// <param name="data"> -> 획득할 아이템 </param>
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
            //  인벤토리가 꽉찼을 때 처리. 방탈출 게임이라 필요한지 생각해볼 필요 있음.
        }
    }

    /// <summary>
    /// 빈슬롯을 가져오는 함수
    /// </summary>
    /// <returns> 빈슬롯이 있으면 해당 슬롯 반환, 빈슬롯이 없으면 null 반환 </returns>
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
    /// 모든 슬롯의 상태를 업데이트 해주는 함수
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
    /// 슬롯을 선택하고 선택된 슬롯의 아이템의 정보를 표기해줌
    /// </summary>
    /// <param name="index"> -> 선택한 아이템 슬롯의 주소 </param>
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
    /// 인벤토리에 아이템이 존재하는 지 찾는 함수
    /// </summary>
    /// <param name="name"> -> 찾고 싶은 아이템의 이름 </param>
    /// <returns> 아이템이 존재하면 그 아이템의 정보를 반환, 아이템이 없으면 null을 반환 </returns>
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
    /// 버튼을 클릭하면 디테일창에 selectedItem 정보를 넘기고, 인벤토리창이 꺼지며 디테일 창이 켜진다
    /// </summary>
    public void OnClickDetailButton()
    {
        detailUI.GetItemInfo(selectedItem);
        detailWindow.SetActive(true);
        InventoryToggle();
    }
}
