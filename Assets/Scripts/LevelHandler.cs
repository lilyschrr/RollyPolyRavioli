
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eFlag;
    [SerializeField] private List<GameObject> stationObjects;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private string sceneToGoTo;


    [Header("Game State")]
    public string adjective;
    public string recipeName;
    public RecipeData selectedRecipe;
    public int score = 0;
    public int currentLevel = 1;
    [SerializeField] public int maxLevels = 3;

    private int currentStationIndex = 0;

    void Start()
    {
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        currentStationIndex = 0;
        score = 0;

        SetUpNextStation(currentStationIndex);
    }

    private void SetUpNextStation(int stationToSetUpNum)
    {
        if (stationToSetUpNum >= 0 &&
            stationToSetUpNum < stationObjects.Count)
        {
            GameObject nextStation = stationObjects[stationToSetUpNum];

            if (nextStation != null && eFlag != null)
            {
                eFlag.SetActive(true);
                eFlag.transform.position =
                    nextStation.transform.position +
                    new Vector3(10f, 400f, 0f);
            }
        }
    }

    // Checks whether this station is the next one in sequence
    public bool CanEnterStation(GameObject station)
    {
        if (currentStationIndex < 0 ||
            currentStationIndex >= stationObjects.Count)
            return false;

        return stationObjects[currentStationIndex] == station;
    }

    // Called when the player finishes interacting with a station
    public void ExitStation(int stationLeavingNum, int pointsEarned)
    {
        // Prevent completing stations out of order
        if (stationLeavingNum != currentStationIndex)
        {
            Debug.LogWarning("Attempted to complete a station out of order!");
            return;
        }

        score += pointsEarned;

        Debug.Log($"Station {stationLeavingNum} completed. Score: {score}");

        // Restore player movement
        if (player != null &&
            player.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.moveSpeed = 400f;
        }

        // Advance the sequence
        currentStationIndex++;

        if (currentStationIndex < stationObjects.Count)
        {
            SetUpNextStation(currentStationIndex);
        }
        else
        {
            AdvanceLevel();
        }
    }

    private void AdvanceLevel()
    {
        currentLevel++;

        if (currentLevel <= maxLevels)
        {
            Debug.Log($"Level Complete! Advancing to Level {currentLevel}");
            StartNewLevel();
        }
        else
        {
            Debug.Log("All levels completed! Victory!");

            if (eFlag != null)
                eFlag.SetActive(false);

            sceneChanger.LoadTargetScene(sceneToGoTo);
        }
    }

    public string GetCurrentStationName()
    {
        if (currentStationIndex >= stationObjects.Count)
            return null;

        return stationObjects[currentStationIndex].name;
    }

    public int GetCurrentStationIndex()
    {
        return currentStationIndex;
    }

    public GameObject GetCurrentStation()
    {
        return stationObjects[currentStationIndex];
    }
}
