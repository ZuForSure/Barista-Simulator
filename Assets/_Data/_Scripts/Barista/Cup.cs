using System;

public class Cup : IngredientContainer
{
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
                GameEvents.UIevents.OnShowGuideUI("You need to add ingredient first");
                break;

            case RecipeResult.Wrong:
                GameEvents.UIevents.OnShowGuideUI("Wrong recipe, press R to reset");
                break;

            case RecipeResult.Correct:
                GameEvents.UIevents.OnShowGuideUI("Done");
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
}
