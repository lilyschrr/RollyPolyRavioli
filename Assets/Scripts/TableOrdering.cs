using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class TableOrdering : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI specialText;
    [SerializeField] private TextMeshProUGUI genericText;
    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private GameObject tableSceneView;
    [SerializeField] public TextMeshProUGUI buttonText;
    [SerializeField] public GameObject recipeImg;
    [SerializeField] private Interactable tableZone;
    
    [Header("Recipe Master List")]
    [SerializeField] private List<RecipeData> allRecipes = new List<RecipeData>();

    private List<string> allAdjectives = new List<string>();

    private void OnEnable()
    {
        int station = levelHandler.GetCurrentStationIndex();

        if (station == 0)
        {
            SetUpOrdering();
        }
        else if (station == 5)
        {
            SetUpServing();
        }
    }

    private void SetUpOrdering()
    {
        buttonText.text = "Begin Level\n" + levelHandler.currentLevel.ToString() + "/" + levelHandler.maxLevels.ToString();
        recipeImg.SetActive(false);

        genericText.text = "I would like a ravioli that is...";
        // compiles list of all unique adjectives from RecipeData assets
        foreach (var recipe in allRecipes)
        {
            if (recipe != null)
            {
                foreach (string adj in recipe.ValidAdjectives)
                {
                    if (!allAdjectives.Contains(adj))
                    {
                        allAdjectives.Add(adj);
                    }
                }
            }
        }

        if (allAdjectives.Count > 0)
        {
            int randIndex = UnityEngine.Random.Range(0, allAdjectives.Count);
            string randAdj = allAdjectives[randIndex];

            // Display adjective clue and save to levelHandler
            levelHandler.adjective = randAdj;
            specialText.text = randAdj;
        }
    }

    private void SetUpServing()
    {   
        buttonText.text = "Finish Level";

        recipeImg.SetActive(true);

        UnityEngine.UI.Image img = recipeImg.GetComponent<UnityEngine.UI.Image>();
        img.sprite = levelHandler.selectedRecipe.FinalMealSprite;

        // Customize text based on score
        genericText.text = "Thank you for the meal! It is...";
        int finalScore = levelHandler.score;
        string finalResponse = finalScore switch
        {
            >= 115 and <= 300 => "PERFECT",
            >= 110 and <= 115 => "Superb!",
            >= 108 and <= 110 => "Amazing",
            >= 105 and <= 108 => "Delicious",
            >= 100 and <= 105 => "Tasty",
            >= 90 and <= 100 => "Yummy",
            >= 85 and <= 90 => "Alright",
            >= 70 and <= 85 => "Fine",
            >= 50 and <= 70 => "Meh",
            >= 20 and <= 50 => "Bad",
            < 20 => "Deplorable",
            _ => "..."
        };
        specialText.text = finalResponse;
        
    }

    public void OnFinish()
    {
        int currentStation = levelHandler.GetCurrentStationIndex();

        tableSceneView.SetActive(false);

        if (currentStation == 0)
        {
            tableZone.ResetInteraction();
            levelHandler.ExitStation(0, 20);
        }
        else if (currentStation == 5)
        {
            tableZone.ResetInteraction();
            levelHandler.ExitStation(5, 0);
        }
    }
}
