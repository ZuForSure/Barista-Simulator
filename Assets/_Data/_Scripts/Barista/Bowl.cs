using System;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : IngredientContainer
{
    public static Action<string> OnNotifyBowl;

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
                OnNotifyBowl("You need to add ingredient first");
                break;

            case RecipeResult.Wrong:
                OnNotifyBowl("Wrong recipe, press R to reset");
                break;

            case RecipeResult.HandFull:
                OnNotifyBowl("Your hand is full");
                break;

            case RecipeResult.Correct:
                OnNotifyBowl("Matcha liquid created!");
                break;
        }
    }

    //protected void TrySpawn()
    //{
    //    if (IsMatchWithSteps(playerSteps, bowlSteps))
    //    {
    //        if (ItemHolder.Instance.IsHolding()) return;

    //        HoldAbleIngredient item = Instantiate(matchaLiquid);
    //        item.gameObject.SetActive(true);
    //        ItemHolder.Instance.HoldItem(item);

    //        ResetContainer();
    //    }
    //}

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
