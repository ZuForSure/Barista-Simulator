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

    [Header("Obj Interact UI")]
    [SerializeField] private GameObject computerPanel;
    [SerializeField] private GameObject billPanel;
    [SerializeField] private GameObject cashDrawerPanel;

    private BillData lastBillData;

    private IngredientContainer currentContainer;

    private HashSet<GameObject> openedUIs = new HashSet<GameObject>();

    protected override void Awake()
    { 
        base.Awake();
        HideGuideUI();
    }

    // ================= UI CONTROL =================
    public void RegisterOpenUI(GameObject ui)
    {
        if (openedUIs.Add(ui))
        {
            Debug.Log("Open UI: " + ui.name);
            SetPlayerControl(false);
        }
    }

    public void RegisterCloseUI(GameObject ui)
    {
        if (openedUIs.Remove(ui))
        {
            Debug.Log("Close UI: " + ui.name);
        }

        if (openedUIs.Count == 0)
        {
            SetPlayerControl(true);
        }
    }

    // ================= GUIDE =================
    public void ShowGuideUI(string message)
    {
        panel.SetActive(true);
        text.text = message;
    }

    public void HideGuideUI()
    {
        panel.SetActive(false);
    }

    // ================= CUP =================
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

    public void HideContainerUI()
    {
        contentParent.gameObject.SetActive(false);
    }

    public void OpenUI(UIType type)
    {
        GameObject target = null;

        switch (type)
        {
            case UIType.Computer:
                target = computerPanel;
                break;

            case UIType.Bill:
                target = billPanel;
                break;

            case UIType.CashDrawer:
                target = cashDrawerPanel;
                break;
        }

        if (target == null) return;

        target.SetActive(true);
        RegisterOpenUI(target);

        if (type == UIType.Bill)
        {
            var data = GetLastBill();
            if (data != null)
            {
                target.GetComponent<BillUI>().Setup(data);
            }
        }
    }

    public void SetLastBill(BillData data)
    {
        lastBillData = data;
    }

    public BillData GetLastBill()
    {
        return lastBillData;
    }

    // ================= PLAYER =================
    private void SetPlayerControl(bool canControl)
    {
        playerController.CanControl = canControl;

        Cursor.lockState = canControl ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !canControl;
    }

    private void OnEnable()
    {
        GameEvents.UIevents.OnUpdateStepContainerUI += UpdateCupUI;
        GameEvents.UIevents.OnAddStepContainerUI += AddTextItem;
        GameEvents.UIevents.OnResetContainerUI += RemoveTextItem;

        GameEvents.UIevents.OnShowGuideUI += ShowGuideUI;
        GameEvents.UIevents.OnHideGuideUI += HideGuideUI;
        GameEvents.UIevents.OnShowContainerUI += ShowContainerUI;
        GameEvents.UIevents.OnHideContainerUI += HideContainerUI;
    }

    private void OnDisable()
    {
        GameEvents.UIevents.OnUpdateStepContainerUI -= UpdateCupUI;
        GameEvents.UIevents.OnAddStepContainerUI -= AddTextItem;
        GameEvents.UIevents.OnResetContainerUI -= RemoveTextItem;

        GameEvents.UIevents.OnShowGuideUI -= ShowGuideUI;
        GameEvents.UIevents.OnHideGuideUI -= HideGuideUI;
        GameEvents.UIevents.OnShowContainerUI -= ShowContainerUI;
        GameEvents.UIevents.OnHideContainerUI -= HideContainerUI;
    }
}
