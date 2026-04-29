using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    protected static RecipeManager instance;
    public static RecipeManager Instance => instance;

    [SerializeField] protected List<HoldAbleRecipe> recipes;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Only 1 RecipeManager can exist");
        RecipeManager.instance = this;
    }

    private void Reset()
    {
        this.LoadRecipes();
    }

    protected void LoadRecipes()
    {
        if (this.recipes.Count > 0) return;

        foreach (Transform child in transform)
        {
            this.recipes.Add(child.gameObject.GetComponent<HoldAbleRecipe>());
        }

        Debug.Log(transform.name + ": LoadRecipes", gameObject);
    }

    public void SpawnRecipe()
    {

    }
}
