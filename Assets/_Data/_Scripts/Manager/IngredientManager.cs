using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] protected List<HoldAbleIngredient> ingredients;

    private void Reset()
    {
        this.LoadIngredientBox();
    }

    protected void LoadIngredientBox()
    {
        if (this.ingredients.Count > 0) return;

        foreach (Transform child in transform)
        {
            this.ingredients.Add(child.gameObject.GetComponent<HoldAbleIngredient>());
        }

        Debug.Log(transform.name + ": LoadIngredientBox", gameObject);
    }
}
