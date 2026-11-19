using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "CookingGame/RecipeData")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private string recipeName;
    [SerializeField] private Sprite recipeSprite;
    [SerializeField] private List<string> validAdjectives = new List<string>();
    [SerializeField] private List<IngredientData> ingredients = new List<IngredientData>();
    [SerializeField] private Sprite finalMealSprite;

    public string RecipeName => recipeName;
    public Sprite RecipeSprite => recipeSprite;
    public List<string> ValidAdjectives => validAdjectives; // Public getter
    public List<IngredientData> Ingredients => ingredients; // Public getter
    public Sprite FinalMealSprite => finalMealSprite;

    public bool MatchesAdjective(string adj)
    {
        return validAdjectives.Contains(adj);
    }
}

[System.Serializable]
public class IngredientData
{
    public string name;
    public Sprite sprite;
}
