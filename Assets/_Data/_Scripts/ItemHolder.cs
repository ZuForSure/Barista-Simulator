using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance;

    [SerializeField] private Transform holdPointSmall, holdPointBig;
    private Item currentItem;
    public bool IsHolding() => currentItem != null;
    public Item CurrentItem => currentItem;

    private void Awake()
    {
        Instance = this;
    }

    public void HoldItem(Item item)
    {
        currentItem = item;

        if (item.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
        }

        if (item.TryGetComponent<Collider>(out var col))
        {
            col.enabled = false;
        }

        Transform targetPoint = item.itemType == ItemType.Big ? holdPointBig : holdPointSmall;

        item.transform.SetParent(targetPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public void TryDrop(RaycastHit hit)
    {
        if (currentItem == null) return;

        if (hit.collider.TryGetComponent<IDropZone>(out var dropZone))
        {
            if (dropZone is DropZone dz && dz.IsOccupied())
            {
                Debug.Log("This place already has an item");
                return;
            }

            dropZone.PlaceItem(currentItem);
            currentItem = null;
        }
        else
        {
            Debug.Log("CANT DROP HERE");
        }
    }
}
