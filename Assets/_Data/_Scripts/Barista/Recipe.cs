using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drink/Recipe")]
public class Recipe : ScriptableObject
{
    public int recipeID;
    public string recipeName;
    public List<RecipeSteps> steps;
    public int price;
}
