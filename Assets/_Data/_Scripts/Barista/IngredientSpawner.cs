using UnityEngine;

public class IngredientSpawner : MonoBehaviour,IInteract
{
    [SerializeField] private HoldAbleIngredient itemPrefab;
    [SerializeField] private string interactText = "Left Click to pick up";

    public string GetInteractText()
    {
        string name = this.itemPrefab.Ingredient.name;
        this.interactText = $" {name}: Left Click to pick up";
        return interactText;
    }

    public void Interact()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        if (ItemHolder.Instance.IsHolding()) return;

        HoldAbleIngredient item = Instantiate(itemPrefab);
        ItemHolder.Instance.HoldItem(item);
    }
}
