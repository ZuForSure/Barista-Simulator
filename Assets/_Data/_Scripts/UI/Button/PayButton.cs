using UnityEngine;

public class PayButton : BaseButton
{
    protected override void HandleClick()
    {
        PrintBill();
        GameEvents.UIevents.OnCloseComputerUI?.Invoke();
        ListOrdersManager.Instance.ClearAllItems();
    }

    private void PrintBill()
    {
        var items = ListOrdersManager.Instance.GetItems();

        Debug.Log("===== BILL =====");

        foreach (var kvp in items)
        {
            var recipe = kvp.Key;
            var ui = kvp.Value;

            int quantity = ui.GetQuantity();
            float price = recipe.price * quantity;

            Debug.Log($"{recipe.name} x{quantity} = {price}.000 VND");
        }

        Debug.Log($"TOTAL: {ListOrdersManager.Instance.GetTotal()} .000 VND");
        Debug.Log("================");
    }
}
