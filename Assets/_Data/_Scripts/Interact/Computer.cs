using UnityEngine;

public class Computer : MonoBehaviour, IInteract
{
    [SerializeField] protected string interactText = "Computer: Left Click to use";

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        GameEvents.UIevents.OnOpenComputerUI?.Invoke();
    }
}
