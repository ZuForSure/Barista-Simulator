using UnityEngine;

public class Item : MonoBehaviour,IInteract
{
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
