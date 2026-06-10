using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] protected Animator anim;

    private void Awake()
    {
        this.anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameEvents.GameplayEvents.OnPrintBill += PrintBillAnim;
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.OnPrintBill -= PrintBillAnim;

    }

    public void PrintBillAnim()
    {
        this.anim.SetTrigger("IsPrintBill");
    }
}
