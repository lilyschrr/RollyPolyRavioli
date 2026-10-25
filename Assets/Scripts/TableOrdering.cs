using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Collections.Generic;
public class TableOrdering : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] LevelHandler levelHandler;
    [SerializeField] GameObject tableSceneView;
    private List<string> adjectives = new List<string> {"cheesy", "smooth", "classic", "fresh", "light", "zesty", "rich", "dense", "refined"};

    void Start()
    {
        int randIndex = Random.Range(0, adjectives.Count);
        string randAdj = adjectives[randIndex];

        // TODO: Add dialogue cutscene later

        // Display adjective clue and save to levelHandler
        levelHandler.adjective = randAdj;
        textField.text = randAdj;
        
    }

    public void OnFinish()
    {
        tableSceneView.SetActive(false);
        levelHandler.ExitStation(0, 20);
    }
}
