public class CratePallet : InteractableObject, IInteractable
{
    public Shelf shelf;

    public string GetInteractionText()
    {
        string str = "'E'키로 선반에 상자 배치하기";

        return str;
    }

    public string GetNameText()
    {
        return "상자더미";
    }

    public void OnInteract()
    {
        SoundManager.Instance.PlaySFX("SFX_InteractPickDrop", transform.position);
        
        InterectWithPallet();
    }

    /// <summary>
    /// Pallet와 상호작용했을 때 상자가 있을 경우 선반의 상자를 증가시키는 기능(최대개수제한)
    /// </summary>
    public void InterectWithPallet()
    {
        if (shelf.crates.Count >= 0 && shelf.activeCount < shelf.crates.Count)
        {
            shelf.crates[shelf.activeCount].SetActive(true);
            shelf.activeCount++;
        }
    }
}
