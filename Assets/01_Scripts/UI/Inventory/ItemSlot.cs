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
    /// 해당 슬롯 갱신
    /// </summary>
   public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    /// <summary>
    /// 해당 슬롯 초기화
    /// </summary>
   public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
    }

    /// <summary>
    /// 슬롯 선택
    /// </summary>
    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
