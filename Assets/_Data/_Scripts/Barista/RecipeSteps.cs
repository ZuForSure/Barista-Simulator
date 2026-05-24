public enum RecipeStepType
{
    AddIngredient = 0,
    Stir = 1,
    Shake = 2,
}

public enum RecipeResult
{
    Empty = 0,
    Wrong = 1,
    Correct = 2,
    HandFull = 3,
}

[System.Serializable]
public class RecipeSteps
{
    public RecipeStepType stepType;

    public Ingredient ingredient;
    public float amount;
}

