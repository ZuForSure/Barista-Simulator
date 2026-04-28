using UnityEngine;

public class IngredientSpawner : MonoBehaviour,IInteract
{
    [SerializeField] private HoldAbleIngredient itemPrefab;

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
