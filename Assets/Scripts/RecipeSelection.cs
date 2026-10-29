using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
public class RecipeSelection : MonoBehaviour
{
    [SerializeField] LevelHandler levelHandler;
    [SerializeField] ImageGallery imageGallery;
    [SerializeField] GameObject bookSceneView;

    private List<string> mushroomAdj = new List<string> {"rich", "dense", "refined"};
    private List<string> cheeseAdj = new List<string> {"cheesy", "smooth", "classic"};
    private List<string> lemonAdj = new List<string> {"fresh", "light", "zesty"};

    private string chosenRecipeName;


    public void OnFinish()
    {
        // retrieve selected recipe index, store in the level handler
        int recipeIndex = imageGallery.CurrentIndex;
        
        switch (recipeIndex)
        {
            case 0: 
                    chosenRecipeName = "Mushroom";
                    break;
            case 1:
                    chosenRecipeName = "Cheese";
                    break;
            case 2:
                    chosenRecipeName = "Lemon";
                    break;
            default:
                    Debug.Log("Recipe index error?");
                    break;
        }

        levelHandler.recipeName = chosenRecipeName;


        // Determine score based on recipe choice
        String adj = levelHandler.adjective;
        int pointsEarned = 10;
        // Score increases if the recipe fits the given adjective
        if (chosenRecipeName.Equals("Mushroom") && mushroomAdj.Contains(adj))
        {
            pointsEarned += 10;
        }
        else if (chosenRecipeName.Equals("Cheese") && cheeseAdj.Contains(adj))
        {
            pointsEarned += 10;
        }
        else if (chosenRecipeName.Equals("Lemon") && lemonAdj.Contains(adj))
        {
           pointsEarned += 10;  
        }


        // Exit & send earned score
        bookSceneView.SetActive(false);
        levelHandler.ExitStation(0, pointsEarned);
    }
}
