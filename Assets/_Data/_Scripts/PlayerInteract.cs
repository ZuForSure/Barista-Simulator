using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static Action<string> OnShowGuide;
    public static Action OnHideGuide;

    [SerializeField] LayerMask cupLayer;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected HoldAbleIngredient currentIngredient;
    [SerializeField] protected Cup currentCup;
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
            currentIngredient?.Use(currentCup, true);
        }

        if (InputManager.Instance.IsAddTopping)
        {
            currentIngredient?.Use(currentCup, false);
        }

        if (InputManager.Instance.IsRemove)
        {
            currentCup.ResetCup();
        }

        //Test Input
        if (Input.GetKeyDown(KeyCode.Y))
        {
            currentCup?.Stir();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentCup?.Shake();
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
        //currentInteractable = null;
        //currentCup = null;

        //if (TryRaycast(out RaycastHit hit, ~0)) // ~0 = all layers
        //{
        //    // Detect Cup
        //    if (((1 << hit.collider.gameObject.layer) & cupLayer) != 0)
        //    {
        //        if (hit.collider.TryGetComponent(out Cup cup))
        //        {
        //            currentCup = cup;
        //        }
        //    }

        //    // Detect Interactable
        //    IInteract interactable = hit.collider.GetComponent<IInteract>();
        //    if (interactable != null)
        //    {
        //        HandleInteractable(interactable);
        //        return;
        //    }
        //}

        //OnHideGuide?.Invoke();
        //Cup.OnHideCupUI?.Invoke();

        IInteract newInteractable = null;
        Cup newCup = null;

        if (TryRaycast(out RaycastHit hit, ~0))
        {
            // Detect cup
            if (((1 << hit.collider.gameObject.layer) & cupLayer) != 0)
            {
                hit.collider.TryGetComponent(out newCup);
            }

            // Detect interactable
            newInteractable = hit.collider.GetComponent<IInteract>();
        }

        if (newInteractable != currentInteractable)
        {
            currentInteractable = newInteractable;
            currentCup = newCup;

            if (currentInteractable != null)
            {
                HandleInteractable(currentInteractable);
            }
            else
            {
                OnHideGuide?.Invoke();
                Cup.OnHideCupUI?.Invoke();
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
        //if (currentInteractable == interactable) return;

        //currentInteractable = interactable;

        //if (interactable is DropZone dropZone)
        //{
        //    OnShowGuide?.Invoke(dropZone.GetInteractText());
        //    return;
        //}

        //if (interactable is Cup && currentIngredient != null)
        //{
        //    OnShowGuide?.Invoke(GetCupInteractText());
        //    Cup.OnShowCupUI?.Invoke();
        //    return;
        //}

        //OnShowGuide?.Invoke(interactable.GetInteractText());

        if (interactable is DropZone dropZone)
        {
            OnShowGuide?.Invoke(dropZone.GetInteractText());
            return;
        }

        if (interactable is Cup)
        {
            if (currentIngredient != null)
                OnShowGuide?.Invoke(GetCupInteractText());
            else
                OnShowGuide?.Invoke("Left Click to make Beverage");

            Cup.OnShowCupUI?.Invoke();
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
