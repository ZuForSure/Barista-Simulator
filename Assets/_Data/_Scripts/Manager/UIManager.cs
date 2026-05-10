using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;

    protected override void Awake()
    { 
        base.Awake();
        Hide();
    }

    public void Show(string message)
    {
        panel.SetActive(true);
        text.text = message;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
