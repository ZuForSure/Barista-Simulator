using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] LayerMask cupLayer;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected IngredientBox currentSyrup;
    [SerializeField] protected Cup currentCup;
    [SerializeField] protected float range = 2f;

    //Bien test
    public Recipe recipe;

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
            this.PouringIngredientIntoCup();
        }

        this.TestCheckRecipe();
    }

    //Ham nay de test chu chua done
    public void TestCheckRecipe()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (this.currentCup.IsMatch(recipe.ingredients, currentCup.CurrentIngredients)) 
            { 
                Debug.Log("CHUC MUNG MAY DA LAM RA LY BAC XIU NCC");
            }
            else
            {
                Debug.LogWarning("SAI CONG THUC ROI THANG NGU");
            }

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

                if (hit.collider.gameObject.CompareTag("Syrup"))
                {
                    GameObject syrupBox = hit.collider.gameObject;
                    this.currentSyrup = syrupBox.GetComponent<IngredientBox>();
                }

                interactObj?.Interact();
            }
        }
    }

    protected void PouringIngredientIntoCup()
    {
        if (ItemHolder.Instance.IsHolding() && ItemHolder.Instance.CurrentItem.gameObject.CompareTag("Syrup") && currentCup != null)
        {
            this.currentSyrup.PouringSyrup();
        }
    }
}
