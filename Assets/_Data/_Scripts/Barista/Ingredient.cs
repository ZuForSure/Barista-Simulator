using UnityEngine;

[CreateAssetMenu(menuName = "Drink/Ingredient")]
public class Ingredient : ScriptableObject
{
    public int IngredientID;
    public IngredientType Type;
    public string ingredientName;
}

public enum IngredientType{
    Syrup = 0,
    Topping = 1,
    CustomSyrup = 2,
    Decor = 3,
}
