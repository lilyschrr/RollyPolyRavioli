using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "CookingGame/RecipeData")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private string recipeName;
    [SerializeField] private Sprite recipeSprite;
    [SerializeField] private List<string> validAdjectives = new List<string>();

    public string RecipeName => recipeName;
    public Sprite RecipeSprite => recipeSprite;
    public List<string> ValidAdjectives => validAdjectives; // Public getter

    public bool MatchesAdjective(string adj)
    {
        return validAdjectives.Contains(adj);
    }
}
