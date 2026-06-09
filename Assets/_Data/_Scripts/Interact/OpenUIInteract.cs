using UnityEngine;

public class OpenUIInteract : MonoBehaviour, IInteract
{
    [SerializeField] private string interactText = "Press to interact";
    [SerializeField] private UIType uiType;

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        UIManager.Instance.OpenUI(uiType);
    }
}

public enum UIType
{
    Computer,
    Bill
}
