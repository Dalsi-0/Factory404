using System.Collections.Generic;
using UnityEngine;

public class Shelf : InteractableObject, IInteractable
{
    [Header("Shelf 위에 있는 상자들 목록")]
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
        string str = "'E'키로 상자 회수하기";

        return str;
    }

    public string GetNameText()
    {
        return "선반";
    }

    public void OnInteract()
    {
        SoundManager.Instance.PlaySFX("SFX_InteractPickDrop", transform.position);
        InterectWithShelf();
    }

    /// <summary>
    /// 선반과 상호작용했을 때 상자가 있을 경우 상자를 줄이는 기능(최소개수제한)
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
