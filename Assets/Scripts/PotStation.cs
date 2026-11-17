using System.Collections.Generic;
using UnityEngine;

public class PotStation : MonoBehaviour
{
    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private GameObject potSceneView;
    [SerializeField] public GameObject ravioliPanel;
    [SerializeField] public RectTransform potPanel;
    [SerializeField] public GameObject boilingRavioli;
    private int toDropCount = 6;
    private int toClickCount = 6;
    private int pointsEarned = 0;

    public void OnSuccessfulDrop()
    {
        // Create a new ravioli spawned in the pot at a random location
        GameObject rav = Instantiate(boilingRavioli, potPanel, false);
        RectTransform ravRect = rav.GetComponent<RectTransform>();

        Rect rect = potPanel.rect;
        float randomX = Random.Range(rect.xMin, rect.xMax);
        float randomY = Random.Range(rect.yMin, rect.yMax);

        ravRect.anchoredPosition = new Vector2(randomX, randomY);

        // TODO: add SFX

        toDropCount --;
        if (toDropCount == 0)
        {
            StartBoiling();
        }
    }

    public void StartBoiling()
    {
        ravioliPanel.gameObject.SetActive(false);

    }

    public void RavioliClicked(int points, GameObject clicked)
    {
        clicked.gameObject.SetActive(false);

        toClickCount --;
        if (toClickCount == 0) CloseStation();
        pointsEarned += points;
    }

    private void CloseStation()
    {
        potSceneView.SetActive(false);
        levelHandler.ExitStation(4, pointsEarned); 
    }
}
