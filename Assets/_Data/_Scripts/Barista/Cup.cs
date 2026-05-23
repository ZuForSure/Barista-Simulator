using System;

public class Cup : IngredientContainer
{
    public static Action OnResetCup;
    public static Action OnShowCupUI;
    public static Action OnHideCupUI;
    public static Action<string> OnNotifyCup;

    public override string GetInteractText()
    {
        this.interactText = "Cup: Left Click to make Beverage";
        return base.GetInteractText();
    }

    public override void Interact()
    {
        var result = CheckRecipe();

        switch (result)
        {
            case RecipeResult.Empty:
                OnNotifyCup?.Invoke("You need to add ingredient first");
                break;

            case RecipeResult.Wrong:
                OnNotifyCup?.Invoke("Wrong Recipe, press R to reset");
                break;

            case RecipeResult.Correct:
                OnNotifyCup?.Invoke("Done");
                break;
        }
    }

    protected RecipeResult CheckRecipe()
    {
        if (playerSteps.Count == 0)
            return RecipeResult.Empty;

        foreach (var r in RecipeManager.Instance.Recipes)
        {
            if (IsMatchWithSteps(r.recipe.steps, playerSteps))
            {
                RecipeManager.Instance.SpawnRecipe(r, transform.position);
                ResetContainer();
                return RecipeResult.Correct;
            }
        }

        return RecipeResult.Wrong;
    }

    public override void ResetContainer()
    {
        base.ResetContainer();
        OnResetCup?.Invoke();
    }
}
