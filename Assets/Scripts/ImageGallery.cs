using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageGallery : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image displayImage; 

    [Header("Gallery Data")]
    [SerializeField] private List<RecipeData> recipeList = new List<RecipeData>(); 

    private int currentIndex = 0; 

    public int CurrentIndex => currentIndex;
    
    // Helper property to fetch currently selected recipe
    public RecipeData CurrentRecipe
    {
        get
        {
            if (recipeList.Count == 0 || currentIndex < 0 || currentIndex >= recipeList.Count)
                return null;
            return recipeList[currentIndex];
        }
    }

    void Start()
    {
        UpdateGalleryDisplay();
    }

    public void ShowNextImage()
    {
        if (recipeList.Count == 0) return;
        currentIndex = (currentIndex + 1) % recipeList.Count;
        UpdateGalleryDisplay();
    }

    public void ShowPreviousImage()
    {
        if (recipeList.Count == 0) return;
        currentIndex = (currentIndex - 1 + recipeList.Count) % recipeList.Count;
        UpdateGalleryDisplay();
    }

    private void UpdateGalleryDisplay()
    {
        if (recipeList.Count > 0 && displayImage != null && CurrentRecipe != null)
        {
            displayImage.sprite = CurrentRecipe.RecipeSprite;
        }
    }
}
