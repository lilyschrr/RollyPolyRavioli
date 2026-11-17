using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoilingObjTimer : MonoBehaviour
{
    [SerializeField] GameObject rav;

    public bool Cooked = false;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }

        float time = Random.Range(4, 9);
        StartCoroutine(BoilTimer(time));
    }

    void OnClick()
    {   
        int points = 1;
        if (Cooked) points = 3;

        PotStation potStation = Object.FindAnyObjectByType<PotStation>();
        if (potStation != null)
        {
            potStation.RavioliClicked(points, gameObject);
        }
    }

    IEnumerator BoilTimer(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        ChangeAppearanceBoiled();
    }

    void ChangeAppearanceBoiled()
    {
        rav.GetComponent<Image>().color = Color.white;
        rav.GetComponent<RectTransform>().localScale.Set(1.5f, 1.5f, 1f);
        Cooked = true;
    }
}
