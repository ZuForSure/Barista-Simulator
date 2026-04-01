using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected SyrupBox currentSyrup;
    [SerializeField] protected float range = 2f;

    void Update()
    {
        this.Interact();
    }

    protected void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.PickUpItem();
        }

        if (Input.GetKey(KeyCode.E))
        {
            this.PouringIngredientIntoCup();
        }
    }

    protected void PickUpItem()
    {
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, this.range))
        {
            if (ItemHolder.Instance.IsHolding())
            {
                ItemHolder.Instance.TryDrop(hit);
            }
            else
            {
                var interactObj = hit.collider.GetComponentInParent<IInteract>();

                if (hit.collider.gameObject.CompareTag("Syrup"))
                {
                    GameObject syrupBox = hit.collider.gameObject;
                    this.currentSyrup = syrupBox.GetComponent<SyrupBox>();
                }

                interactObj?.Interact();
            }
        }
    }

    protected void PouringIngredientIntoCup()
    {
        if (ItemHolder.Instance.IsHolding() && ItemHolder.Instance.CurrentItem.gameObject.CompareTag("Syrup"))
        {
            this.currentSyrup.Pouring();
        }
    }
}
