using UnityEngine;
public enum ItemType
{
    Small = 0,
    Big = 1,
    Ingredient = 2,
}

public enum PlaceableType
{
    None = 0,
    Table = 1,
    Chair = 2,
}

public class Item : MonoBehaviour,IInteract
{
    [Header("Item")]
    public ItemType itemType;
    public PlaceableType placeableType;
    private bool isLocked = false;
    [SerializeField] private DropZone currentZone;
    [SerializeField] protected string interactText = "Left Click to pick up";

    public virtual string GetInteractText()
    {
        if (isLocked) return "";
        return interactText;
    }

    public void Interact()
    {
        this.PickUpItem();
    }

    public void LockItem()
    {
        isLocked = true;
    }

    public virtual void PickUpItem()
    {
        if (isLocked)
        {
            Debug.Log("Item is locked, cannot pick up!");
            return;
        }

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
