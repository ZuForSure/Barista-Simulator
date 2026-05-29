using UnityEngine;

public class QuitBtnComputerUI : MonoBehaviour
{
    public void OnClickQuit()
    {
        UIManager.OnCloseComputer?.Invoke();
    }
}
