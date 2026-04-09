using UnityEngine;

public class DropZone : MonoBehaviour,IDropZone
{
    [SerializeField] private Transform placePoint;
    [SerializeField] private Item currentItem;
    public bool IsOccupied() => currentItem != null;

    public void PlaceItem(Item item)
    {
        if (currentItem != null) return;
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
    }

    public void RemoveItem()
    {
        currentItem = null;
    }
}
