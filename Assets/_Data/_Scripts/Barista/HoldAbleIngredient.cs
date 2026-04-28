using UnityEngine;

public class HoldAbleIngredient : Item
{
    [Header("HoldAbleIngredient")]
    [SerializeField] protected Ingredient ingredient;
    [SerializeField] protected float amoutPerSec = 20f;
    [SerializeField] protected Cup cup;
    [SerializeField] private bool isConsumable = false;

    public void Use(Cup cup, bool isHolding)
    {
        if (ingredient.Type == IngredientType.Syrup)
        {
            cup.AddIngredient(ingredient, Time.deltaTime * amoutPerSec);
        }
        else if (ingredient.Type == IngredientType.Topping && !isHolding)
        {
            cup.AddIngredient(ingredient, 1);

            if (isConsumable)
            {
                Consume();
            }
        }
    }

    protected void Consume()
    {
        ItemHolder.Instance.ForceClear();
        Destroy(gameObject);
    }
}
