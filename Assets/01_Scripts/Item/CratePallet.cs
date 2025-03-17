public class CratePallet : InteractableObject, IInteractable
{
    public Shelf shelf;

    public string GetInteractionText()
    {
        string str = "'E'Ű�� ���� ��ġ�ϱ�";

        return str;
    }

    public string GetNameText()
    {
        return "���ڴ���";
    }

    public void OnInteract()
    {
        InterectWithPallet();
    }

    /// <summary>
    /// Pallet�� ��ȣ�ۿ����� �� ���ڰ� ���� ��� ������ ���ڸ� ������Ű�� ���(�ִ밳������)
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
