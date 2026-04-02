using UnityEngine;

public class IngredientBox : Item
{
    [SerializeField] protected Ingredient ingredient;
    [SerializeField] protected float amoutPerSec = 20f;
    [SerializeField] protected Cup cup;

    public void PouringSyrup()
    {
        cup.AddIngredient(ingredient, Time.deltaTime * amoutPerSec);
    }

    public void AddTopping()
    {
        cup.AddIngredient(ingredient, 1);
    }
}
