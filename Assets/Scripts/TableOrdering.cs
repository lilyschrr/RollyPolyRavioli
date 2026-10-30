using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TableOrdering : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private GameObject tableSceneView;
    
    [Header("Recipe Master List")]
    [SerializeField] private List<RecipeData> allRecipes = new List<RecipeData>();

    private List<string> allAdjectives = new List<string>();

    void Start()
    {
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
            int randIndex = Random.Range(0, allAdjectives.Count);
            string randAdj = allAdjectives[randIndex];

            // Display adjective clue and save to levelHandler
            levelHandler.adjective = randAdj;
            textField.text = randAdj;
        }
        else
        {
            Debug.LogWarning("No adjectives found! Make sure RecipeData assets are assigned in TableOrdering.");
        }
    }

    public void OnFinish()
    {
        tableSceneView.SetActive(false);
        levelHandler.ExitStation(0, 20);
    }
}
