using UnityEngine;

public class PayButton : BaseButton
{
    [SerializeField] private GameObject billPrefab;
    [SerializeField] private Transform billParent;

    protected override void HandleClick()
    {
        PrintBill();
        GameEvents.UIevents.OnCloseComputerUI?.Invoke();
        ListOrdersManager.Instance.ClearAllItems();
    }

    private void PrintBill()
    {
        var items = ListOrdersManager.Instance.GetItems();

        BillData data = new BillData();
        data.shopName = "MATCHA STORE";
        data.time = System.DateTime.Now.ToString("HH:mm dd/MM/yyyy");

        //Debug.Log("===== BILL =====");

        foreach (var kvp in items)
        {
            var recipe = kvp.Key;
            var ui = kvp.Value;

            int quantity = ui.GetQuantity();
            float price = recipe.price * quantity;

            data.items.Add(new BillItemData
            {
                itemName = recipe.name,
                quantity = quantity,
                price = (int)recipe.price
            });

            Debug.Log($"{recipe.name} x{quantity} = {price}.000 VND");
        }

        data.total = (int)ListOrdersManager.Instance.GetTotal();

        // Spawn UI bill
        var billGO = Instantiate(billPrefab, billParent);
        billGO.GetComponent<BillUI>().Setup(data);

        //Debug.Log($"TOTAL: {ListOrdersManager.Instance.GetTotal()} .000 VND");
        //Debug.Log("================");
    }
}
