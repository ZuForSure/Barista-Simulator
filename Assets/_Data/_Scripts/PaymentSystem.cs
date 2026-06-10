using UnityEngine;

public class PaymentSystem : MonoBehaviour
{
    [SerializeField] private GameObject computerUI;

    private void OnEnable()
    {
        GameEvents.GameplayEvents.OnPay += HandlePay;
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.OnPay -= HandlePay;
    }

    private void HandlePay(BillData data)
    {
        UIManager.Instance.RegisterCloseUI(computerUI);
        computerUI.SetActive(false);

        UIManager.Instance.SetLastBill(data);

        GameEvents.GameplayEvents.OnPrintBill?.Invoke();
    }
}
