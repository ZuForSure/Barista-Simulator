using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IngredientContainer : MonoBehaviour, IInteract
{
    [Header("Ingredient Container")]
    public static Action<int, string> OnUpdateStep;
    public static Action<string> OnAddStep;

    [SerializeField] protected List<RecipeSteps> playerSteps;
    [SerializeField] protected string interactText;
    public List<RecipeSteps> PlayerSteps => playerSteps;

    public virtual string GetInteractText()
    {
        return interactText;
    }

    public virtual void Interact()
    {
        throw new System.NotImplementedException();
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
        }
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

    public virtual void ResetContainer()
    {
        playerSteps.Clear();
    }
}
