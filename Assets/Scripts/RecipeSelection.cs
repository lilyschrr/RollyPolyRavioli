using UnityEngine;

public class RecipeSelection : MonoBehaviour
{
    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private ImageGallery imageGallery;
    [SerializeField] private GameObject bookSceneView;
    

    public void OnFinish()
    {
        RecipeData selectedRecipe = imageGallery.CurrentRecipe;

        if (selectedRecipe == null)
        {
            Debug.LogError("Selected recipe index error or gallery is empty!");
            return;
        }

        // Store chosen recipe name in LevelHandler
        levelHandler.recipeName = selectedRecipe.RecipeName;
        levelHandler.selectedRecipe = selectedRecipe;

        // Determine score dynamically using ScriptableObject's logic
        string currentAdjective = levelHandler.adjective;
        int pointsEarned = 10;

        if (selectedRecipe.MatchesAdjective(currentAdjective))
        {
            pointsEarned += 10;
        }

        // Exit & send earned score
        bookSceneView.SetActive(false);
        levelHandler.ExitStation(1, pointsEarned);
    }
}
