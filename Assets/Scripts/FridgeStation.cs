using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FridgeStation : MonoBehaviour
{
    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private GameObject fridgeSceneView;
    [SerializeField] private TextMeshProUGUI textField;


    private List<string> requiredIngredients = new List<string>();
    private List<string> collectedIngredients = new List<string>();
    private int pointsEarned = 20;

    void Start()
    {
        // Pull required ingredients from the chosen RecipeData
        if (levelHandler != null && levelHandler.selectedRecipe != null)
        {
            requiredIngredients = new List<string>(levelHandler.selectedRecipe.Ingredients);
            
            // configure text
            foreach (string str in requiredIngredients)
            {
                textField.text += "\n- " + str;
            }
            
            Debug.Log($"Fridge loaded recipe: {levelHandler.selectedRecipe.RecipeName} with {requiredIngredients.Count} ingredients required.");
        }
    }

    public void OnIngredientClicked(string ingredientName, GameObject clickedButtonObject)
    {
        // Check if the clicked item is needed and hasn't been collected
        if (requiredIngredients.Contains(ingredientName) && !collectedIngredients.Contains(ingredientName))
        {
            collectedIngredients.Add(ingredientName);

            // Hide the button
            clickedButtonObject.SetActive(false);

            // When complete:
            if (collectedIngredients.Count >= requiredIngredients.Count)
            {
                CompleteFridgeStage();
            }
        }
        else
        {
            // Deduct minor points
            if (pointsEarned > 0) pointsEarned --;
        }
    }

    private void CompleteFridgeStage()
    {
        fridgeSceneView.SetActive(false);
        levelHandler.ExitStation(2, pointsEarned); 
    }
}