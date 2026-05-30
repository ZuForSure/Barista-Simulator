using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button btnAdd;
    [SerializeField] private Button btnRemove;

    private Recipe recipe;
    private int quantity = 1;
    private System.Action<Recipe, int> onValueChanged;
    public int GetQuantity() => quantity;
    public Recipe GetRecipe() => recipe;

    public void Setup(Recipe recipe, System.Action<Recipe, int> callback)
    {
        this.recipe = recipe;
        this.onValueChanged = callback;

        btnAdd.onClick.AddListener(OnAdd);
        btnRemove.onClick.AddListener(OnRemove);

        UpdateUI();
    }

    private void OnAdd()
    {
        quantity++;
        UpdateUI();
        onValueChanged?.Invoke(recipe, quantity);
    }

    private void OnRemove()
    {
        quantity--;
        UpdateUI();
        onValueChanged?.Invoke(recipe, quantity);

        if (quantity <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateUI()
    {
        text.text = $"{recipe.recipeName} x{quantity}: {recipe.price * quantity}k";
    }
}
