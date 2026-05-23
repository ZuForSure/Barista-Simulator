using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Guide UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Cup UI")]
    [SerializeField] private Cup cup;
    [SerializeField] private Transform contentParent;
    [SerializeField] private TextMeshProUGUI stepItemText;
    [SerializeField] private List<GameObject> newPrefabs;

    protected override void Awake()
    { 
        base.Awake();
        HideUIGuide();
    }

    public void ShowUIGuide(string message)
    {
        panel.SetActive(true);
        text.text = message;
    }

    public void HideUIGuide()
    {
        panel.SetActive(false);
    }

    public void UpdateCupUI(int index, string message)
    {
        if (index < 0 || index >= newPrefabs.Count) return;

        newPrefabs[index].GetComponent<TextMeshProUGUI>().text = message;
    }

    public void AddTextItem(string message)
    {
        if (!contentParent.gameObject.activeSelf)
            contentParent.gameObject.SetActive(true);

        GameObject go = Instantiate(stepItemText.gameObject, contentParent);
        go.GetComponent<TextMeshProUGUI>().text = message;
        newPrefabs.Add(go);
    }

    public void RemoveTextItem()
    {
        foreach (var go in newPrefabs)
        {
            Destroy(go);
        }

        newPrefabs.Clear();
        contentParent.gameObject.SetActive(false);
    }

    public void ShowCupUI()
    {
        contentParent.gameObject.SetActive(true);
    }

    public void HideCupUI()
    {
        contentParent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        IngredientContainer.OnUpdateStep += UpdateCupUI;
        IngredientContainer.OnAddStep += AddTextItem;
        IngredientContainer.OnResetContainer += RemoveTextItem;

        PlayerInteract.OnShowGuide += ShowUIGuide;
        PlayerInteract.OnHideGuide += HideUIGuide;

        IngredientContainer.OnShowContainerUI += ShowCupUI;
        IngredientContainer.OnHideContainerUI += HideCupUI;

        Cup.OnNotifyCup += ShowUIGuide;
    }

    private void OnDisable()
    {
        IngredientContainer.OnUpdateStep -= UpdateCupUI;
        IngredientContainer.OnAddStep -= AddTextItem;
        IngredientContainer.OnResetContainer -= RemoveTextItem;

        PlayerInteract.OnShowGuide -= ShowUIGuide;
        PlayerInteract.OnHideGuide -= HideUIGuide;

        IngredientContainer.OnShowContainerUI -= ShowCupUI;
        IngredientContainer.OnHideContainerUI -= HideCupUI;

        Cup.OnNotifyCup -= ShowUIGuide;

    }
}
