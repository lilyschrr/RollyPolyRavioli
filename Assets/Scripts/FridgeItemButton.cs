using UnityEngine;
using UnityEngine.UI;

public class FridgeItemButton : MonoBehaviour
{
    [SerializeField] private FridgeStation fridgeStationPrefab;
    [SerializeField] private string ingredientName;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        if (fridgeStationPrefab != null)
        {
            fridgeStationPrefab.OnIngredientClicked(ingredientName, gameObject);
        }
    }
}