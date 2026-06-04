using TMPro;
using UnityEngine;

public class BillUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private Transform contentItems;
    [SerializeField] private TextMeshProUGUI txtTotal;
    [SerializeField] private TextMeshProUGUI txtFooter;

    [Header("Prefab")]
    [SerializeField] private GameObject itemPrefab;

    public void Setup(BillData data)
    {
        txtTitle.text = data.shopName;
        txtTime.text = data.time;
        txtFooter.text = data.footer;

        foreach (Transform child in contentItems)
        {
            Destroy(child.gameObject);
        }

        int total = 0;

        foreach (var item in data.items)
        {
            var go = Instantiate(itemPrefab, contentItems);
            var txt = go.GetComponent<TextMeshProUGUI>();

            int price = item.price * item.quantity;
            total += price;

            txt.text = $"{item.itemName} x{item.quantity} - {price}đ";
        }

        txtTotal.text = $"TOTAL: {total}đ";
    }
}
