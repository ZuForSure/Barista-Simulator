using UnityEngine;

public class OptionButton : BaseButton
{
    [SerializeField] protected Recipe recipe;
    protected override void HandleClick()
    {
        GameEvents.Order.OnSelectRecipe?.Invoke(recipe);
    }
}
