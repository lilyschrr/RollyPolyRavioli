using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class CounterStation : MonoBehaviour
{
[SerializeField] LevelHandler levelHandler;
[SerializeField] private GameObject counterSceneView;
[SerializeField] HorizontalLayoutGroup inventoryPanel;
[SerializeField] GameObject dragIngredient;


private List<IngredientData> requiredIngredients = new List<IngredientData>();

private int pointsEarned = 40;


    void Start()
    {
        // Load ingredients list as sprites within inventory panel
        // Change unique ingredients' sprites according to recipe chosen:

        // Pull required ingredients from the chosen RecipeData
        if (levelHandler != null && levelHandler.selectedRecipe != null)
        {
            requiredIngredients = new List<IngredientData>(levelHandler.selectedRecipe.Ingredients);
            foreach (IngredientData ingr in requiredIngredients)
            {
                
            }
        }

    }

    public void OnSuccessfulDrop()
    {
        // Award points
        // SFX
        // Update bowl content appearance
    }

    // When done, begin mixing interactions

    // Lay out dough & roll
    // add filling
    // add layer & cut

    // exit

}
