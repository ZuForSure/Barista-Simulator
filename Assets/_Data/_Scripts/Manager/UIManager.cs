using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private ElmanGameDevTools.PlayerSystem.PlayerController playerController;

    [Header("Guide UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Cup UI")]
    [SerializeField] private Transform contentParent;
    [SerializeField] private TextMeshProUGUI stepItemText;
    [SerializeField] private List<GameObject> newPrefabs;

    [Header("Computer UI")]
    [SerializeField] private GameObject computerPanel;
    public static Action OnCloseComputer;

    private IngredientContainer currentContainer;

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

    public void ShowContainerUI(IngredientContainer container)
    {
        if (currentContainer != container)
        {
            currentContainer = container;

            RemoveTextItem();
            foreach (var step in container.PlayerSteps)
            {
                if (step.ingredient != null)
                {
                    AddTextItem($"{step.ingredient.name}: {step.amount}");
                }
                else
                {
                    AddTextItem(step.stepType.ToString());
                }
            }
        }

        contentParent.gameObject.SetActive(true);
    }

    public void HideCupUI()
    {
        contentParent.gameObject.SetActive(false);
    }

    public void ShowComputerUI()
    {
        computerPanel.SetActive(true);
        SetPlayerControl(false);
    }

    public void HideComputerUI()
    {
        computerPanel.SetActive(false);
        SetPlayerControl(true);
    }

    private void SetPlayerControl(bool canControl)
    {
        playerController.CanControl = canControl;

        Cursor.lockState = canControl ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !canControl;
    }

    private void OnEnable()
    {
        IngredientContainer.OnUpdateStep += UpdateCupUI;
        IngredientContainer.OnAddStep += AddTextItem;
        IngredientContainer.OnResetContainer += RemoveTextItem;

        PlayerInteract.OnShowGuide += ShowUIGuide;
        PlayerInteract.OnHideGuide += HideUIGuide;

        IngredientContainer.OnShowContainerUI += ShowContainerUI;
        IngredientContainer.OnHideContainerUI += HideCupUI;

        Cup.OnNotifyCup += ShowUIGuide;
        Bowl.OnNotifyBowl += ShowUIGuide;
        Computer.OnOpenComputer += ShowComputerUI;
        OnCloseComputer += HideComputerUI;
    }

    private void OnDisable()
    {
        IngredientContainer.OnUpdateStep -= UpdateCupUI;
        IngredientContainer.OnAddStep -= AddTextItem;
        IngredientContainer.OnResetContainer -= RemoveTextItem;

        PlayerInteract.OnShowGuide -= ShowUIGuide;
        PlayerInteract.OnHideGuide -= HideUIGuide;

        IngredientContainer.OnShowContainerUI -= ShowContainerUI;
        IngredientContainer.OnHideContainerUI -= HideCupUI;

        Cup.OnNotifyCup -= ShowUIGuide;
        Bowl.OnNotifyBowl -= ShowUIGuide;
        Computer.OnOpenComputer -= ShowComputerUI;
        OnCloseComputer -= HideComputerUI;
    }
}
