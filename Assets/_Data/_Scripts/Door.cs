using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        this.Open();
    }

    public void Open()
    {
        animator.SetTrigger("IsOpen");
    }
}
