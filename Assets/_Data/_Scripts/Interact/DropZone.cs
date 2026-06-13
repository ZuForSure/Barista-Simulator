using UnityEngine;

public class DropZone : MonoBehaviour,IDropZone, IInteract
{
    [SerializeField] private PlaceableType allowType;
    [SerializeField] private bool lockAfterPlace = true;
    [SerializeField] private Transform placePoint;
    [SerializeField] private Item currentItem;
    public PlaceableType AllowType => allowType;

    public string GetInteractText()
    {
        if (ItemHolder.Instance.IsHolding())
        {
            return "Left Click to Drop";
        }
        else
        {
            return "Place item here";
        }
    }

    public void Interact()
    {
        
    }

    public bool IsOccupied() => currentItem != null;

    public void PlaceItem(Item item)
    {
        if (currentItem != null) return;

        if (item.placeableType != allowType)
        {
            Debug.Log("Wrong placeable type!");
            return;
        }

        currentItem = item;

        item.transform.SetParent(null);
        item.transform.position = placePoint.position;
        item.transform.rotation = placePoint.rotation;
        item.SetDropZone(this);

        if (item.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
        }

        if (item.TryGetComponent<Collider>(out var col))
        {
            col.enabled = true;
        }

        if (lockAfterPlace)
        {
            item.LockItem();
        }
    }

    public void RemoveItem()
    {
        currentItem = null;
    }
}
