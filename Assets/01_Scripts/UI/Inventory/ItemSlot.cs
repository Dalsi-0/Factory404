using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;

    public InventoryUI inventory;

    public int index;
    public bool selected;
    public int quantity;

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
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }

    /// <summary>
    /// 해당 슬롯 초기화
    /// </summary>
    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    /// <summary>
    /// 슬롯 선택
    /// </summary>
    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
