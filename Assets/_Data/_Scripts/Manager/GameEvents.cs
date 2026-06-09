using System;
using UnityEngine;

public static class GameEvents
{
    // ================= ORDER =================
    public static class Order
    {
        public static Action<Recipe> OnSelectRecipe;
        public static Action<Recipe, int> OnItemChanged;
        public static Action OnClearAllItems;
    }

    //================== UI ====================
    public static class UIevents
    {
        public static Action<string> OnShowGuideUI;
        public static Action OnHideGuideUI;

        //Containter: Cup and Bowl
        public static Action<IngredientContainer> OnShowContainerUI;
        public static Action OnHideContainerUI;
        public static Action<int, string> OnUpdateStepContainerUI;
        public static Action<string> OnAddStepContainerUI;
        public static Action OnResetContainerUI;
    }

    //================== GAMEPLAY ====================
    public static class GameplayEvents
    {
        public static Action<HoldAbleIngredient> OnHoldIngredient;
        public static Action OnDropItem;
    }
}
