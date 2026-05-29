using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static Action<string> OnShowGuide;
    public static Action OnHideGuide;

    [SerializeField] LayerMask containerLayer;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected HoldAbleIngredient currentIngredient;
    [SerializeField] protected IngredientContainer currentContainer;
    [SerializeField] protected float range = 2f;
    protected IInteract currentInteractable;

    void Update()
    {
        this.Detect();
        this.Interact();
    }

    protected void Interact()
    {
        if (InputManager.Instance.IsLeftClick)
        {
            this.PickUpItem();
        }

        if (InputManager.Instance.IsPouring)
        {
            currentIngredient?.Use(currentContainer, true);
        }

        if (InputManager.Instance.IsAddTopping)
        {
            currentIngredient?.Use(currentContainer, false);
        }

        if (InputManager.Instance.IsRemove)
        {
            currentContainer?.ResetContainer();
        }

        //Test Input
        if (Input.GetKeyDown(KeyCode.Y))
        {
            currentContainer?.Stir();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentContainer?.Shake();
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
            }
        }
    }

    private string GetCupInteractText()
    {
        if (currentIngredient == null) return "Cup";

        switch (currentIngredient.Ingredient.Type)
        {
            case IngredientType.Syrup:
                return "Hold E to Pour";

            case IngredientType.Topping:
                return "Press E to Add Topping";

            default:
                return "Interact with Cup";
        }
    }

    void Detect()
    {
        IInteract newInteractable = null;
        IngredientContainer newContainer = null;

        if (TryRaycast(out RaycastHit hit, ~0))
        {
            // Detect container
            if (((1 << hit.collider.gameObject.layer) & containerLayer) != 0)
            {
                hit.collider.TryGetComponent(out newContainer);
            }

            // Detect interactable
            newInteractable = hit.collider.GetComponent<IInteract>();
        }

        if (newInteractable != currentInteractable || newContainer != currentContainer)
        {
            currentInteractable = newInteractable;
            currentContainer = newContainer;

            if (currentInteractable != null)
            {
                HandleInteractable(currentInteractable);
            }
            else
            {
                OnHideGuide?.Invoke();
                IngredientContainer.OnHideContainerUI?.Invoke();
            }
        }
    }

    private bool TryRaycast(out RaycastHit hit, LayerMask mask)
    {
        return Physics.Raycast(
            mainCam.transform.position,
            mainCam.transform.forward,
            out hit,
            range,
            mask
        );
    }

    void HandleInteractable(IInteract interactable)
    {
        if (interactable is DropZone dropZone)
        {
            OnShowGuide?.Invoke(dropZone.GetInteractText());
            return;
        }

        //if (interactable is IngredientContainer)
        //{
        //    if (currentIngredient != null)
        //        OnShowGuide?.Invoke(GetCupInteractText());
        //    else
        //        OnShowGuide?.Invoke("Left Click to make Beverage");

        //    IngredientContainer.OnShowContainerUI?.Invoke();
        //    return;
        //}

        if (interactable is IngredientContainer container)
        {
            currentContainer = container;

            if (currentIngredient != null)
                OnShowGuide?.Invoke(GetCupInteractText());
            else
                OnShowGuide?.Invoke(container.GetInteractText());

            IngredientContainer.OnShowContainerUI?.Invoke(container);
            return;
        }

        OnShowGuide?.Invoke(interactable.GetInteractText());
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
