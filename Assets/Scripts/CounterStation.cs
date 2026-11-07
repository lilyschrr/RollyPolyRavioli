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
[SerializeField] GameObject dragPrefab;
[SerializeField] RectTransform doughBowl;
[SerializeField] RectTransform fillingBowl;



private List<IngredientData> requiredIngredients = new List<IngredientData>();

private int pointsEarned = 40;


    void Start()
    {
        // Load ingredients list as draggable sprite prefabs within inventory panel
        if (levelHandler != null && levelHandler.selectedRecipe != null)
        {
            // Pull required ingredients from the chosen RecipeData
            requiredIngredients = new List<IngredientData>(levelHandler.selectedRecipe.Ingredients);
            foreach (IngredientData ingr in requiredIngredients)
            {
                // Create draggable prefab & set image
                GameObject ingredient = Instantiate(dragPrefab, inventoryPanel.transform, false);
                ingredient.transform.Find("Image").GetComponent<Image>().sprite = ingr.sprite;
                
                // Set bowl target based on ingredient
                if (ingr.name.Equals("Egg") || ingr.name.Equals("Milk") || ingr.name.Equals("Flour"))
                {
                    // Target = Dough bowl
                    ingredient.GetComponentInChildren<Draggable>().targetDropZone = doughBowl;
                }
                else
                {
                    // Target = Filling bowl
                    ingredient.GetComponentInChildren<Draggable>().targetDropZone = fillingBowl;
                }
                
                // Force the Layout Group to rebuild
                LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryPanel.GetComponent<RectTransform>());
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryPanel.GetComponent<RectTransform>());
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
