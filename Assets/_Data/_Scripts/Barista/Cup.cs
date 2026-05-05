using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteract
{
    [SerializeField] protected List<IngredientAmount> currentIngredients;
    public List<IngredientAmount> CurrentIngredients => currentIngredients;

    protected bool isStirred;
    protected bool isShaken;

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
        isStirred = true;
        Debug.Log("Stirred!");
    }

    public void Shake()
    {
        isShaken = true;
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
        switch (action)
        {
            case FinishAction.None:
                return true;
            case FinishAction.Stir:
                return isStirred;
            case FinishAction.Shake:
                return isShaken;
        }
        return false;
    }

    public void ResetCup()
    {
        currentIngredients.Clear();
        isStirred = false;
        isShaken = false;
    }
}
