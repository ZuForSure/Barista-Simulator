using System;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteract
{
    public static Action<int, string> OnUpdateStep;
    public static Action<string> OnAddStep;
    public static Action OnResetCup;
    public static Action OnShowCupUI;
    public static Action OnHideCupUI;
    public static Action<string> OnNotifyCup;

    [SerializeField] protected List<RecipeSteps> playerSteps;
    public List<RecipeSteps> PlayerSteps => playerSteps;

    [SerializeField] private string interactText = "Cup: Left Click to make Beverage";

    public void Interact()
    {
        //this.CheckRecipeAndSpawn();

        var result = CheckRecipe();

        switch (result)
        {
            case RecipeResult.Empty:
                OnNotifyCup?.Invoke("You need to add ingredient first");
                break;

            case RecipeResult.Wrong:
                OnNotifyCup?.Invoke("Wrong Recipe, press R to reset Cup");
                break;

            case RecipeResult.Correct:
                OnNotifyCup?.Invoke("Done");
                break;
        }
    }

    public void AddIngredient(Ingredient ingredient, float amount)
    {
        var existing = playerSteps.Find(i => i.ingredient == ingredient);

        if (existing != null)
        {
            existing.amount += amount;

            int index = playerSteps.IndexOf(existing);

            OnUpdateStep?.Invoke(index, $"{existing.ingredient.name}: {existing.amount}");
        }
        else
        {
            playerSteps.Add(new RecipeSteps
            {
                stepType = RecipeStepType.AddIngredient,
                ingredient = ingredient,
                amount = amount
            });

            OnAddStep?.Invoke($"{ingredient.name}: {amount}ml");

            //UIManager.Instance.AddTextItem($"{ingredient.name}: {amount}ml");
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

    //protected void CheckRecipeAndSpawn()
    //{
    //    foreach (var r in RecipeManager.Instance.Recipes)
    //    {
    //        if (!IsMatchWithSteps(r.recipe.steps, playerSteps)) continue;

    //        RecipeManager.Instance.SpawnRecipe(r, transform.position);
    //        this.ResetCup();
    //        return;
    //    }

    //    Debug.LogWarning("Wrong Recipe");
    //}

    protected RecipeResult CheckRecipe()
    {
        if (playerSteps.Count == 0)
            return RecipeResult.Empty;

        foreach (var r in RecipeManager.Instance.Recipes)
        {
            if (IsMatchWithSteps(r.recipe.steps, playerSteps))
            {
                RecipeManager.Instance.SpawnRecipe(r, transform.position);
                ResetCup();
                return RecipeResult.Correct;
            }
        }

        return RecipeResult.Wrong;
    }

    public void ResetCup()
    {
        playerSteps.Clear();
        OnResetCup?.Invoke();

        //UIManager.Instance.RemoveTextItem();
    }

    public string GetInteractText()
    {
        return interactText;
    }
}
