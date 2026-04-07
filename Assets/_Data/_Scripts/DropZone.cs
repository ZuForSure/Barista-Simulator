using UnityEngine;

public class DropZone : MonoBehaviour,IDropZone
{
    [SerializeField] private Transform placePoint;

    void Awake()
    {
        //placePoint = transform.GetComponentInChildren<Transform>();
    }

    public void PlaceItem(Item item)
    {
        item.transform.SetParent(null);

        item.transform.position = placePoint.position;
        item.transform.rotation = placePoint.rotation;

        if (item.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
        }

        if (item.TryGetComponent<Collider>(out var col))
        {
            col.enabled = true;
        }
    }
}
