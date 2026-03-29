using UnityEngine;

public class Item : MonoBehaviour,IInteract
{
    public void Interact()
    {
        this.PickUpItem();
    }

    public void PickUpItem()
    {
        ItemHolder.Instance.HoldItem(this);
    }
}
