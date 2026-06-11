using UnityEngine;

public class PayButton : BaseButton
{
    [SerializeField] private GameObject billPrefab;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private Transform billParent;

    protected override void HandleClick()
    {

        var data = BuildBillData();
        GameEvents.GameplayEvents.OnPay?.Invoke(data);

        ListOrdersManager.Instance.ClearAllItems();
    }

    private BillData BuildBillData()
    {
        var items = ListOrdersManager.Instance.GetItems();

        BillData data = new()
        {
            time = System.DateTime.Now.ToString("HH:mm dd/MM/yyyy")
        };

        foreach (var kvp in items)
        {
            var recipe = kvp.Key;
            var ui = kvp.Value;

            int quantity = ui.GetQuantity();

            data.items.Add(new BillItemData
            {
                itemName = recipe.recipeName,
                quantity = quantity,
                price = (int)recipe.price
            });
        }

        data.total = (int)ListOrdersManager.Instance.GetTotal();

        return data;
    }
}
