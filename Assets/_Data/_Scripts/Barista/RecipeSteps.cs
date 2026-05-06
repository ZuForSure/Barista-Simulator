public enum RecipeStepType
{
    AddIngredient = 0,
    Stir = 1,
    Shake = 2,
}

[System.Serializable]
public class RecipeSteps
{
    public RecipeStepType stepType;

    public Ingredient ingredient;
    public float amount;
}

