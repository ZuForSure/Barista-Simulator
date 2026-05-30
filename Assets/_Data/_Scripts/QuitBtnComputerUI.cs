public class QuitBtnComputerUI : BaseButton
{
    protected override void HandleClick()
    {
        UIManager.OnCloseComputer?.Invoke();
    }
}
