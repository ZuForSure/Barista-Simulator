using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteract
{
    [SerializeField] protected List<RecipeSteps> playerSteps;
    public List<RecipeSteps> PlayerSteps => playerSteps;

    //public FinishAction lastAction = FinishAction.None;

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
        //lastAction = FinishAction.Stir;

        playerSteps.Add(new RecipeSteps
        {
            stepType = RecipeStepType.Stir
        });
        Debug.Log("Stirred!");
    }

    public void Shake()
    {
        //lastAction = FinishAction.Shake;

        playerSteps.Add(new RecipeSteps
        {
            stepType = RecipeStepType.Shake,
        });
        Debug.Log("Shaken!");
    }

    //public bool IsMatch(List<RecipeSteps> recipe, List<RecipeSteps> currentInCup)
    //{
    //    foreach (var r in recipe)
    //    {
    //        var i = currentInCup.Find(x => x.ingredient == r.ingredient);

    //        if (i == null) return false;

    //        if (Mathf.Abs(i.amount - r.amount) > 5f)
    //            return false;
    //    }

    //    if (recipe.Count != currentInCup.Count) return false;
    //    return true;
    //}

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
            //bool match = r.recipe.requireOrder
            //? IsMatchWithSteps(r.recipe.steps, PlayerSteps)
            //: IsMatch(r.recipe.steps, PlayerSteps);

            //if (!match) continue;
            //if (!IsFinishActionValid(r.recipe.finishAction))
            //{
            //    Debug.LogWarning("Missing finish step!");
            //    return;
            //}

            if (!IsMatchWithSteps(r.recipe.steps, playerSteps)) continue;

            RecipeManager.Instance.SpawnRecipe(r, transform.position);
            this.ResetCup();
            return;
        }

        Debug.LogWarning("Wrong Recipe");
    }

    //bool IsFinishActionValid(FinishAction action)
    //{
    //    if (action == FinishAction.None) return true;

    //    return lastAction == action;
    //}

    public void ResetCup()
    {
        playerSteps.Clear();
        //currentIngredients.Clear();
        //playerSteps.Clear();
    }
}
