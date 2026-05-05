using System.Collections.Generic;
using UnityEngine;
public enum FinishAction
{
    None,
    Stir,
    Shake
}

[CreateAssetMenu(menuName = "Drink/Recipe")]
public class Recipe : ScriptableObject
{
    public int recipeID;
    public string recipeName;
    public List<IngredientAmount> ingredients;
    public bool requireOrder;
    public FinishAction finishAction;
}
