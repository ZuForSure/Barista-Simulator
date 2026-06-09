using UnityEngine;

public class CloseButtonUI : BaseButton
{
    [SerializeField] private GameObject targetUI;

    protected override void HandleClick()
    {
        UIManager.Instance.RegisterCloseUI(targetUI);
        targetUI.SetActive(false);
    }
}
