public class RemoveAllBtn : BaseButton
{
    protected override void HandleClick()
    {
        GameEvents.Order.OnClearAllItems?.Invoke();
    }
}
