using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FridgeStation : MonoBehaviour
{
    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private Interactable fridgeZone;
    [SerializeField] private GameObject fridgeSceneView;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private List<GameObject> allFridgeContents;


    private List<IngredientData> requiredIngredients = new List<IngredientData>();
    private List<string> ingrNames = new List<string>();
    private List<string> collectedIngredients = new List<string>();
    private int pointsEarned = 20;

    void Start()
    {
        // Pull required ingredients from the chosen RecipeData
        if (levelHandler != null && levelHandler.selectedRecipe != null)
        {
            requiredIngredients = new List<IngredientData>(levelHandler.selectedRecipe.Ingredients);
            
            // configure text
            foreach (IngredientData ingr in requiredIngredients)
            {
                textField.text += "\n- " + ingr.name;
                ingrNames.Add(ingr.name); // create list of required names
            }
            
            Debug.Log($"Fridge loaded recipe: {levelHandler.selectedRecipe.RecipeName} with {requiredIngredients.Count} ingredients required.");
        }
    }

    public void OnIngredientClicked(string ingr, GameObject clickedButtonObject)
    {
        // Check if the clicked item is needed and hasn't been collected
        if (ingrNames.Contains(ingr) && !collectedIngredients.Contains(ingr))
        {
            collectedIngredients.Add(ingr);

            // Hide the button
            clickedButtonObject.SetActive(false);

            // When complete:
            if (collectedIngredients.Count >= ingrNames.Count)
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
        fridgeZone.ResetInteraction();
        levelHandler.ExitStation(2, pointsEarned); 
    }
}