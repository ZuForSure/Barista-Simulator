using TMPro;
using UnityEngine;

public class CashInputSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTotal;

    private int currentTotal = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        currentTotal += amount;
        UpdateUI();
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
}
