using UnityEngine;

public class BoilWaterSpawner : MonoBehaviour, IInteract
{
    [SerializeField] protected Bowl bowl;
    [SerializeField] protected Ingredient boilWater;
    [SerializeField] protected string interactText = "Left click to Add boil water";


    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        this.AddBoidWaterIntoBowl();
    }

    protected void AddBoidWaterIntoBowl()
    {
        this.bowl.AddIngredient(boilWater, 1);
    }
}
