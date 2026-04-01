using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public List<IngredientAmount> currentIngredients = new();

    public void AddIngredient(Ingredient ingredient, float amount)
    {
        var existing = currentIngredients.Find(i => i.ingredient == ingredient);

        if (existing != null)
        {
            existing.amount += amount;
        }
        else
        {
            currentIngredients.Add(new IngredientAmount
            {
                ingredient = ingredient,
                amount = amount
            });
        }

        Debug.Log($"Added {amount}ml of {ingredient.name}");
    }

    bool IsMatch(List<IngredientAmount> recipe, List<IngredientAmount> currentInCup)
    {
        foreach (var r in recipe)
        {
            var i = currentInCup.Find(x => x.ingredient == r.ingredient);

            if (i == null) return false;

            if (Mathf.Abs(i.amount - r.amount) > 5f)
                return false;
        }

        return true;
    }
}
