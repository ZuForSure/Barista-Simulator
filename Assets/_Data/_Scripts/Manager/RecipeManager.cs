using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> recipes;

    private void Reset()
    {
        this.LoadRecipes();
    }

    protected void LoadRecipes()
    {
        if (this.recipes.Count > 0) return;

        foreach (Transform child in transform)
        {
            this.recipes.Add(child.gameObject);
        }

        Debug.Log(transform.name + ": LoadRecipes", gameObject);
    }
}
