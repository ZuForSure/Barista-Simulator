using System;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance;
    public static Action<HoldAbleIngredient> OnHoldIngredient;
    public static Action OnDropItem;

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

        if (item is HoldAbleIngredient ingredient)
        {
            OnHoldIngredient?.Invoke(ingredient);
        }

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
            OnDropItem?.Invoke();
        }
        else
        {
            Debug.Log("CANT DROP HERE");
        }
    }

    public void ForceClear()
    {
        currentItem = null;
        OnDropItem?.Invoke();
    }
}
