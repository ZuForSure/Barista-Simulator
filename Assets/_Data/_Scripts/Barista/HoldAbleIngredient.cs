using UnityEngine;

public class HoldAbleIngredient : Item
{
    [Header("HoldAbleIngredient")]
    [SerializeField] protected Ingredient ingredient;
    [SerializeField] protected float amoutPerSec = 20f;
    [SerializeField] protected Cup cup;

    public void Use(Cup cup, bool isHolding)
    {
        if (ingredient.Type == IngredientType.Syrup)
        {
            cup.AddIngredient(ingredient, Time.deltaTime * amoutPerSec);
        }
        else if (ingredient.Type == IngredientType.Topping && !isHolding)
        {
            cup.AddIngredient(ingredient, 1);
        }
    }
}
