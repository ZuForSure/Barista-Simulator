using UnityEngine;

public class SyrupBox : Item
{

    [SerializeField] protected Ingredient ingredient;
    [SerializeField] protected float amoutPerSec = 20f;
    [SerializeField] protected Cup cup;
    public Ingredient IngredientType => ingredient;

    public void Pouring()
    {
        cup.AddIngredient(ingredient, Time.deltaTime * amoutPerSec);
    }
}
