using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListOrdersManager : Singleton<ListOrdersManager>
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject itemPayPrefab;
    [SerializeField] private TextMeshProUGUI totalPrice;

    private float currentTotal = 0;
    private Dictionary<Recipe, ItemPayUI> items = new();

    public void AddItemPay(Recipe recipe)
    {
        if (items.ContainsKey(recipe))
        {
            items[recipe].SendMessage("OnAdd");
            return;
        }

        GameObject go = Instantiate(itemPayPrefab, contentParent);
        ItemPayUI itemUI = go.GetComponent<ItemPayUI>();

        itemUI.Setup(recipe, OnItemValueChanged);

        items.Add(recipe, itemUI);

        currentTotal += recipe.price;
        UpdateTotalUI();
    }

    private void OnItemValueChanged(Recipe recipe, int quantity)
    {
        RecalculateTotal();

        if (quantity <= 0)
        {
            items.Remove(recipe);
        }
    }

    private void RecalculateTotal()
    {
        currentTotal = 0;

        foreach (var kvp in items)
        {
            var ui = kvp.Value;
            currentTotal += ui.GetRecipe().price * ui.GetQuantity();
        }

        UpdateTotalUI();
    }

    private void UpdateTotalUI()
    {
        totalPrice.text = $"Total: {currentTotal}.000 VND";
    }

    private void OnEnable()
    {
        GameEvents.Order.OnSelectRecipe += AddItemPay;
    }

    private void OnDisable()
    {
        GameEvents.Order.OnSelectRecipe -= AddItemPay;
    }
}
