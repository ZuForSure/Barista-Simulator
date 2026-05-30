using UnityEngine;

public class OptionButton : BaseButton
{
    [SerializeField] protected Recipe recipe;
    protected override void HandleClick()
    {
        GameEvents.OnSelectRecipe?.Invoke(recipe);
    }
}
