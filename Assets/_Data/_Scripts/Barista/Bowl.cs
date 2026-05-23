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
        this.TrySpawn();
    }

    protected void TrySpawn()
    {
        if (IsMatchWithSteps(playerSteps, bowlSteps))
        {
            if (ItemHolder.Instance.IsHolding()) return;

            HoldAbleIngredient item = Instantiate(matchaLiquid);
            item.gameObject.SetActive(true);
            ItemHolder.Instance.HoldItem(item);

            ResetContainer();
        }
    }
}
