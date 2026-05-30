using System;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : IngredientContainer
{
    [Header("Bowl")]
    [SerializeField] protected List<RecipeSteps> bowlSteps;
    [SerializeField] protected HoldAbleIngredient matchaLiquid;

    public override string GetInteractText()
    {
        this.interactText = "Bowl: Left Click to make Matcha Liquid";
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

            case RecipeResult.HandFull:
                GameEvents.UIevents.OnShowGuideUI("Your hand is full");
                break;

            case RecipeResult.Correct:
                GameEvents.UIevents.OnShowGuideUI("Matcha liquid created!");
                break;
        }
    }

    protected RecipeResult CheckRecipe()
    {
        if (playerSteps.Count == 0)
            return RecipeResult.Empty;

        if (ItemHolder.Instance.IsHolding())
            return RecipeResult.HandFull;

        if (IsMatchWithSteps(playerSteps, bowlSteps))
        {
            HoldAbleIngredient item = Instantiate(matchaLiquid);
            item.gameObject.SetActive(true);
            ItemHolder.Instance.HoldItem(item);
            ResetContainer();

            return RecipeResult.Correct;
        }

        return RecipeResult.Wrong;
    }
}
