public class QuitBtnComputerUI : BaseButton
{
    protected override void HandleClick()
    {
        GameEvents.UIevents.OnCloseComputerUI?.Invoke();
    }
}
