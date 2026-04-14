using UnityEngine;
public enum KeyType
{
    Red,
    Blue,
    Gold
}

public class Key : Item
{
    public override void PickUpItem()
    {
        this.PickUpKey();
    }

    protected void PickUpKey()
    {
        PlayerInventory.Instance.hasKey = true;
        Debug.Log("You Have Been Picked Up Key");
        gameObject.SetActive(false);
    }
}
