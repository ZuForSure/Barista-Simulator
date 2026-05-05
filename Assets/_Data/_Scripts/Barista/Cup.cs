using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteract
{
    [SerializeField] protected List<IngredientAmount> currentIngredients;
    public List<IngredientAmount> CurrentIngredients => currentIngredients;

    public FinishAction lastAction = FinishAction.None;

    public void Interact()
    {
        this.CheckRecipeAndSpawn();
    }

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

    public void Stir()
    {
        lastAction = FinishAction.Stir;
        Debug.Log("Stirred!");
    }

    public void Shake()
    {
        lastAction = FinishAction.Shake;
        Debug.Log("Shaken!");
    }

    public bool IsMatch(List<IngredientAmount> recipe, List<IngredientAmount> currentInCup)
    {
        foreach (var r in recipe)
        {
            var i = currentInCup.Find(x => x.ingredient == r.ingredient);

            if (i == null) return false;

            if (Mathf.Abs(i.amount - r.amount) > 5f)
                return false;
        }

        if (recipe.Count != currentInCup.Count) return false;
        return true;
    }

    public bool IsMatchWithOrder(List<IngredientAmount> recipe, List<IngredientAmount> currentInCup)
    {
        if (recipe.Count != currentInCup.Count) return false;

        for (int i = 0; i < recipe.Count; i++)
        {
            var r = recipe[i];
            var c = currentInCup[i];

            if (r.ingredient != c.ingredient) return false;

            if (Mathf.Abs(r.amount - c.amount) > 5f)
                return false;
        }

        return true;
    }

    protected void CheckRecipeAndSpawn()
    {
        foreach (var r in RecipeManager.Instance.Recipes)
        {
            bool match = r.recipe.requireOrder
            ? IsMatchWithOrder(r.recipe.ingredients, CurrentIngredients)
            : IsMatch(r.recipe.ingredients, CurrentIngredients);

            if (!match) continue;

            if (!IsFinishActionValid(r.recipe.finishAction))
            {
                Debug.LogWarning("Missing finish step!");
                return;
            }

            RecipeManager.Instance.SpawnRecipe(r, transform.position);
            this.ResetCup();
            return;
        }

        Debug.LogWarning("Wrong Recipe");
    }

    bool IsFinishActionValid(FinishAction action)
    {
        if (action == FinishAction.None) return true;

        return lastAction == action;
    }

    public void ResetCup()
    {
        currentIngredients.Clear();
        lastAction = FinishAction.None;
    }
}
