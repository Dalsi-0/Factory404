using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public Sprite icon;
    public GameObject itemPrefab;
    public GameObject detailPrefab;
    public string itemName;
    public string itemDescription;

    [Header("Stackable")]
    public bool canStack;
    public int maxStackAmount;
}
