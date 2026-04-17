using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isRequiredKey, isOpen;

    private void Awake()
    {
        //this.animator = GetComponent<Animator>();
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
