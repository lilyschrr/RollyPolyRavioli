using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class CounterStation : MonoBehaviour
{
[SerializeField] LevelHandler levelHandler;
[SerializeField] private GameObject counterSceneView;
[SerializeField] HorizontalLayoutGroup inventoryPanel;
[SerializeField] GameObject dragPrefab;
[SerializeField] GameObject BowlLabels;
[SerializeField] RectTransform doughBowl;
[SerializeField] RectTransform fillingBowl;

[SerializeField] Image contents1, contents2, doughToRoll, rolledDough, rolledDough2;
[SerializeField] Slider spoon1, spoon2, rollingPin;
[SerializeField] RectTransform fillingPlotPanel;
[SerializeField] GameObject cuttingSlidersParent;
[SerializeField] List<Slider> cuttingSliders;
[SerializeField] GameObject allSquaresParent;

private int toDropCount;
private bool mixed1 = false;
private bool mixed2 = false;

private List<IngredientData> requiredIngredients = new List<IngredientData>();

private int fillingToDrop = 6;
private int pointsEarned = 40;
private int cutsToMake = 3;


    void Start()
    {
        // Load ingredients list as draggable sprite prefabs within inventory panel
        if (levelHandler != null && levelHandler.selectedRecipe != null)
        {
            // Pull required ingredients from the chosen RecipeData
            requiredIngredients = new List<IngredientData>(levelHandler.selectedRecipe.Ingredients);
            toDropCount = requiredIngredients.Count +1; // "flour" is the +1
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
        BowlLabels.SetActive(false);
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
     if (spoon2.value == 1)
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
        doughBowl.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1600, 8), 1.0f)
            .SetEase(Ease.InOutQuad) .OnComplete(() => RollingInteraction());
        fillingBowl.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1600, 8), 0.5f)
            .SetEase(Ease.InOutQuad) .OnComplete(() => doughToRoll.gameObject.SetActive(true));
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
        fillingPlotPanel.gameObject.SetActive(true);

    }

    public void OnFillingClicked(Sprite sprite, GameObject clickedButtonObject)
    {
        // Change appearance & remove button component
        Image bImg = clickedButtonObject.GetComponent<Image>();
        bImg.sprite = sprite;
        Button bc = clickedButtonObject.GetComponent<Button>();
        TMP_Text txt = clickedButtonObject.GetComponentInChildren<TMP_Text>();
        if (txt != null) txt.text = "";

        if (bc != null)
        {
            Destroy(bc);
        }

        // If all dropped, move on
        fillingToDrop --;
        if (fillingToDrop == 0)
        {
            AddDoughLayer();
        }
    }


    private void AddDoughLayer()
    {
        rolledDough2.gameObject.SetActive(true);
        rolledDough2.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 19), 1.0f)
            .SetEase(Ease.InOutQuad) .OnComplete(() => CuttingInteraction());
    }

    private void CuttingInteraction()
    {
        cuttingSlidersParent.SetActive(true);
        foreach (Slider slider in cuttingSliders)
        {
            slider.onValueChanged.AddListener(delegate {CutValueChangeCheck(slider); });
        }
    }

    public void CutValueChangeCheck(Slider slider)
    {
     if (slider.value == 1)
        {
            slider.gameObject.SetActive(false);
            cutsToMake--;
        }

    if (cutsToMake == 0)
        {
            StartCoroutine(ClosingStation());
        }
    }

    IEnumerator ClosingStation()
    {
        allSquaresParent.gameObject.SetActive(true);
        doughToRoll.gameObject.SetActive(false);
        rolledDough.gameObject.SetActive(false);
        rolledDough2.gameObject.SetActive(false);

        // SFX
        yield return new WaitForSecondsRealtime(2f);

        counterSceneView.SetActive(false);
        levelHandler.ExitStation(3, pointsEarned); 
    }

}
