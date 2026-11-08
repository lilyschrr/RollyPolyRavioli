using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class CounterStation : MonoBehaviour
{
[SerializeField] LevelHandler levelHandler;
[SerializeField] private GameObject counterSceneView;
[SerializeField] HorizontalLayoutGroup inventoryPanel;
[SerializeField] GameObject dragPrefab;
[SerializeField] RectTransform doughBowl;
[SerializeField] RectTransform fillingBowl;

[SerializeField] Image contents1, contents2, doughToRoll, rolledDough;
[SerializeField] Slider spoon1, spoon2, rollingPin;

private int toDropCount;
private bool mixed1 = false;
private bool mixed2 = false;

private List<IngredientData> requiredIngredients = new List<IngredientData>();

private int pointsEarned = 40;


    void Start()
    {
        // Load ingredients list as draggable sprite prefabs within inventory panel
        if (levelHandler != null && levelHandler.selectedRecipe != null)
        {
            // Pull required ingredients from the chosen RecipeData
            requiredIngredients = new List<IngredientData>(levelHandler.selectedRecipe.Ingredients);
            toDropCount = requiredIngredients.Count;
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
        // TODO: add SFX
       
        toDropCount --;
        if (toDropCount == 0)
        {
            SetupMixingInteraction();
        }

    }

    // When all drops are made, setup mixing interactions
    private void SetupMixingInteraction()
    {
        doughBowl.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-447, 8), 0.5f)
            .SetEase(Ease.InOutQuad) .OnComplete(() => contents1.gameObject.SetActive(true));
        fillingBowl.GetComponent<RectTransform>().DOAnchorPos(new Vector2(503, 8), 0.5f)
            .SetEase(Ease.InOutQuad) .OnComplete(() => contents2.gameObject.SetActive(true));

        MixingInteraction();
    }

    public void MixingInteraction()
    {
        spoon1.gameObject.SetActive(true);
        spoon2.gameObject.SetActive(true);
        spoon1.onValueChanged.AddListener(delegate {SpoonValueChangeCheck(); });
        spoon2.onValueChanged.AddListener(delegate {SpoonValueChangeCheck(); });
    }

    public void SpoonValueChangeCheck()
    {
     if (spoon1.value == 1)
     {
        mixed1 = true; spoon1.gameObject.SetActive(false);
     }
     if (spoon2.value == 2)
     {
        mixed2 = true; spoon2.gameObject.SetActive(false);
     }
     // Proceed when both are done
     if (mixed1 && mixed2)
        {
            spoon1.gameObject.SetActive(false);
            spoon2.gameObject.SetActive(false);
            SetupRollingInteraction();
        }
    }

    private void SetupRollingInteraction()
    {
        doughBowl.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1600, 8), 0.5f)
            .SetEase(Ease.InOutQuad) .OnComplete(() => doughToRoll.gameObject.SetActive(true));
        fillingBowl.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1600, 8), 0.5f)
            .SetEase(Ease.InOutQuad);
        RollingInteraction();
    }

    public void RollingInteraction()
    {
        rollingPin.gameObject.SetActive(true);
        rollingPin.onValueChanged.AddListener(delegate {RollingValueChangeCheck(); });

    }

    public void RollingValueChangeCheck()
    {
     if (rollingPin.value == 1)
     {
        rolledDough.gameObject.SetActive(true);
        rollingPin.gameObject.SetActive(false);
        FillingInteraction();
     }
     
    }

    public void FillingInteraction()
    {
        Debug.Log("Filling station now!");
    }

    // Lay out dough & roll
    // add filling
    // add layer & cut

    // exit

}
