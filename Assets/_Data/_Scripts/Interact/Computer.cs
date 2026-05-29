using System;
using UnityEngine;

public class Computer : MonoBehaviour, IInteract
{
    public static Action OnOpenComputer;
    [SerializeField] protected string interactText = "Computer: Left Click to use";

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        OnOpenComputer?.Invoke();
    }
}
