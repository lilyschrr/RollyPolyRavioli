using System.Collections.Generic;
using UnityEngine;


public class LevelHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eFlag;
    [SerializeField] private List<GameObject> stationObjects; // Scene station GameObjects


    [Header("Game State")]
    public string recipeName;
    public int score = 0;
    public int currentLevel = 1;
    [SerializeField] private int maxLevels = 3;
   
    private int currentStationIndex = 0;


    void Start()
    {
        StartNewLevel();
    }


    /// Initializes or resets stations for level's start
    public void StartNewLevel()
    {
        currentStationIndex = 0;


        // Set up the first station in the sequence
        SetUpNextStation(currentStationIndex);
    }



    /// Sets up next station and places eFlag above
    private void SetUpNextStation(int stationToSetUpNum)
    {
        if (stationToSetUpNum < stationObjects.Count)
        {
            GameObject nextStation = stationObjects[stationToSetUpNum];
           
            if (nextStation != null)
            {
                // Position the eFlag at the station's location
                if (eFlag != null)
                {
                    eFlag.SetActive(true);
                    eFlag.transform.position = nextStation.transform.position + new Vector3(10f, 400f, 0f);
                }
            }
        }
    }


    /// Call  when the player finishes interacting with a station
    public void ExitStation(int stationLeavingNum, int pointsEarned)
    {
        // Update score
        score += pointsEarned;
        Debug.Log($"Station {stationLeavingNum} completed. Score updated to: {score}");


        // Restore player movement speed
        if (player != null && player.GetComponent<PlayerMovement>() != null)
        {
            player.GetComponent<PlayerMovement>().moveSpeed = 8f;
        }

        // Determine the next station in sequence
        currentStationIndex = stationLeavingNum + 1;


        if (currentStationIndex < stationObjects.Count)
        {
            // Move to the next station in the current level
            SetUpNextStation(currentStationIndex);
        }
        else
        {
            // All stations in this level are finished, move to the next level
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
            {
                eFlag.SetActive(false);
            }
            // TODO: Trigger Win/result Screen here
        }
    }
}
