using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance;

    [SerializeField] private Transform holdPoint;
    private Item currentItem;

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

        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }
}
