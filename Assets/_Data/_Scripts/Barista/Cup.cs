using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteract
{
    [SerializeField] protected List<RecipeSteps> playerSteps;
    public List<RecipeSteps> PlayerSteps => playerSteps;

    public void Interact()
    {
        this.CheckRecipeAndSpawn();
    }

    public void AddIngredient(Ingredient ingredient, float amount)
    {
        var existing = playerSteps.Find(i => i.ingredient == ingredient);

        if (existing != null)
        {
            existing.amount += amount;
        }
        else
        {
            playerSteps.Add(new RecipeSteps
            {
                stepType = RecipeStepType.AddIngredient,
                ingredient = ingredient,
                amount = amount
            });
        }

        Debug.Log($"Added {amount}ml of {ingredient.name}");
    }

    public void Stir()
    {
        playerSteps.Add(new RecipeSteps
        {
            stepType = RecipeStepType.Stir
        });
        Debug.Log("Stirred!");
    }

    public void Shake()
    {
        playerSteps.Add(new RecipeSteps
        {
            stepType = RecipeStepType.Shake,
        });
        Debug.Log("Shaken!");
    }

    public bool IsMatchWithSteps(List<RecipeSteps> recipeSteps, List<RecipeSteps> playerSteps)
    {
        if (recipeSteps.Count != playerSteps.Count) return false;

        for (int i = 0; i < recipeSteps.Count; i++)
        {
            var r = recipeSteps[i];
            var p = playerSteps[i];

            if (r.ingredient != p.ingredient) return false;

            if (Mathf.Abs(r.amount - p.amount) > 5f)
                return false;
        }

        return true;
    }

    protected void CheckRecipeAndSpawn()
    {
        foreach (var r in RecipeManager.Instance.Recipes)
        {
            if (!IsMatchWithSteps(r.recipe.steps, playerSteps)) continue;

            RecipeManager.Instance.SpawnRecipe(r, transform.position);
            this.ResetCup();
            return;
        }

        Debug.LogWarning("Wrong Recipe");
    }

    public void ResetCup()
    {
        playerSteps.Clear();
    }

    public string GetInteractText()
    {
        throw new System.NotImplementedException();
    }
}
