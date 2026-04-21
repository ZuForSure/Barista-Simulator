using UnityEngine;
public enum ItemType
{
    Small = 0,
    Big = 1,
}

public class Item : MonoBehaviour,IInteract
{
    public ItemType itemType;
    [SerializeField] private DropZone currentZone;

    public void Interact()
    {
        this.PickUpItem();
    }

    public virtual void PickUpItem()
    {
        if (currentZone != null)
        {
            currentZone.RemoveItem();
            currentZone = null;
        }

        ItemHolder.Instance.HoldItem(this);
    }

    public void SetDropZone(DropZone zone)
    {
        currentZone = zone;
    }
}
