using TMPro;
using UnityEngine;

public class BillUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private Transform contentItems;
    [SerializeField] private TextMeshProUGUI txtTotal;

    [Header("Prefab")]
    [SerializeField] private GameObject itemPrefab;

    private void OnEnable()
    {
        UIManager.Instance.RegisterOpenUI(gameObject);
    }

    private void OnDisable()
    {
        UIManager.Instance.RegisterCloseUI(gameObject);
    }

    public void Setup(BillData data)
    {
        txtTime.text = data.time;
        int total = 0;

        foreach (var item in data.items)
        {
            var go = Instantiate(itemPrefab, contentItems);
            var txt = go.GetComponent<TextMeshProUGUI>();

            int price = item.price * item.quantity;
            total += price;

            txt.text = $"{item.itemName} x{item.quantity} - {price}.000 VND";
        }

        txtTotal.text = $"TOTAL: {total}.000 VND";
    }
}
