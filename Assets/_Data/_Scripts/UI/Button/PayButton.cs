using UnityEngine;

public class PayButton : BaseButton
{
    [SerializeField] private GameObject billPrefab;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private Transform billParent;

    protected override void HandleClick()
    {
        UIManager.Instance.RegisterCloseUI(computerUI);
        computerUI.SetActive(false);

        PrintBill();
        ListOrdersManager.Instance.ClearAllItems();
    }

    private void PrintBill()
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
            _ = recipe.price * quantity;

            data.items.Add(new BillItemData
            {
                itemName = recipe.recipeName,
                quantity = quantity,
                price = (int)recipe.price
            });
        }

        data.total = (int)ListOrdersManager.Instance.GetTotal();

        var billGO = Instantiate(billPrefab, billParent);
        billGO.GetComponent<BillUI>().Setup(data);
    }
}
