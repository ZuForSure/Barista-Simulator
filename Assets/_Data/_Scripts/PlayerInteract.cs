using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] LayerMask cupLayer;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected HoldAbleIngredient currentIngredient;
    [SerializeField] protected Cup currentCup;
    [SerializeField] protected float range = 2f;

    void Update()
    {
        this.Interact();
        this.DetectCup();
    }

    protected void Interact()
    {
        if (InputManager.Instance.IsLeftClick)
        {
            this.PickUpItem();
        }

        if (InputManager.Instance.IsPouring)
        {
            currentIngredient?.Use(currentCup, true);
        }

        if (InputManager.Instance.IsAddTopping)
        {
            currentIngredient?.Use(currentCup, false);
        }

        if (InputManager.Instance.IsRemove)
        {
            currentCup.RemoveAllIngredients();
        }
    }

    protected void DetectCup()
    {
        currentCup = null;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, range, cupLayer))
        {
            if (hit.collider.TryGetComponent(out Cup cup))
            {
                currentCup = cup;
            }
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
                interactObj?.Interact();

                //var syrup = hit.collider.GetComponent<HoldAbleIngredient>();
                //if (syrup != null)
                //{
                //    currentIngredient = syrup;
                //}
            }
        }
    }

    private void OnEnable()
    {
        ItemHolder.OnHoldIngredient += SetCurrentIngredient;
        ItemHolder.OnDropItem += ClearIngredient;
    }

    private void OnDisable()
    {
        ItemHolder.OnHoldIngredient -= SetCurrentIngredient;
        ItemHolder.OnDropItem -= ClearIngredient;
    }

    public void SetCurrentIngredient(HoldAbleIngredient ingredient)
    {
        currentIngredient = ingredient;
    }

    private void ClearIngredient()
    {
        currentIngredient = null;
    }
}
