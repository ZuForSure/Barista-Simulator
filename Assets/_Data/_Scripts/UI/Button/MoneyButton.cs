using UnityEngine;

public class MoneyButton : BaseButton
{
    [SerializeField] private int amount;
    [SerializeField] private CashInputSystem cashSystem;

    protected override void HandleClick()
    {
        cashSystem.AddMoney(amount);
    }
}
