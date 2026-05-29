using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isRequiredKey, isOpen;
    [SerializeField] private string interactText = "Left Click: Open";

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        if (isOpen) return;

        if (isRequiredKey && !PlayerInventory.Instance.hasKey)
        {
            Debug.Log("Need Key");
            return;
        }

        this.Open();
    }

    public void Open()
    {
        isOpen = true;
        animator.SetTrigger("IsOpen");
    }
}
