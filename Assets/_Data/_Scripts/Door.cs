using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }


    public void Open()
    {
        animator.SetTrigger("IsOpen");
    }
}
