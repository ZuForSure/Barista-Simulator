using TMPro;
using UnityEngine;

public class CashInputSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTotal;
    [SerializeField] private TextMeshProUGUI txtBillTotal;

    private int currentTotal = 0, billTotal = 0;

    private void Start()
    {
        UpdateUI();
        UpdateBillUI();
    }

    public void AddMoney(int amount)
    {
        currentTotal += amount;
        UpdateUI();
    }

    public void SetBillTotal(int total)
    {
        billTotal = total;
        UpdateBillUI();
    }

    public void ResetMoney()
    {
        currentTotal = 0;
        UpdateUI();
    }

    public void ConfirmCash()
    {
        Debug.Log("Customer paid: " + currentTotal);
        currentTotal = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        txtTotal.text = "Total: "+ currentTotal.ToString() + ".000VND";
    }

    private void UpdateBillUI()
    {
        txtBillTotal.text = "Bill: " + billTotal + ".000VND";
    }

    private void OnEnable()
    {
        GameEvents.GameplayEvents.OnPay += OnReceiveBill;
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.OnPay -= OnReceiveBill;
    }

    private void OnReceiveBill(BillData data)
    {
        SetBillTotal(data.total);
    }
}
