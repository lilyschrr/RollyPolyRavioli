using UnityEngine;
using UnityEngine.UI;

public class FillingButtonController : MonoBehaviour
{
    [SerializeField] private CounterStation counterStation;
    [SerializeField] private Sprite sprite;

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
        if (counterStation != null)
        {
            counterStation.OnFillingClicked(sprite, gameObject);
        }
    }
}