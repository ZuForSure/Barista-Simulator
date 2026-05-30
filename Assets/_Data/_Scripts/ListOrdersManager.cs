using TMPro;
using UnityEngine;

public class ListOrdersManager : Singleton<ListOrdersManager>
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject itemPayPrefab;
    [SerializeField] private TextMeshProUGUI totalPrice;
    private float currentTotal = 0;

    public void AddItemPay(Recipe recipe)
    {
        GameObject go = Instantiate(itemPayPrefab, contentParent);

        TextMeshProUGUI txt = go.GetComponent<TextMeshProUGUI>();
        txt.text = $"{recipe.recipeName}: {recipe.price}k VND";

        currentTotal += recipe.price;
        UpdateTotalUI();
    }

    private void UpdateTotalUI()
    {
        totalPrice.text = $"Total: {currentTotal}.000 VND";
    }

    private void OnEnable()
    {
        GameEvents.OnSelectRecipe += AddItemPay;
    }

    private void OnDisable()
    {
        GameEvents.OnSelectRecipe -= AddItemPay;
    }
}
