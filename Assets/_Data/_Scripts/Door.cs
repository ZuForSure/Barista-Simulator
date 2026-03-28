using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    public void Open()
    {
        animator.SetTrigger("IsOpen");
    }
}
