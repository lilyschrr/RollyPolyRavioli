using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    [Header("All Stations' Prefabs")]
    [SerializeField] GameObject table;
    [SerializeField] GameObject book;
    [SerializeField] GameObject fridge;
    [SerializeField] GameObject counter;
    [SerializeField] GameObject pot;
    [SerializeField] GameObject serve;


        public bool justStarting = true;
        public bool tableDone = false;
        public bool bookDone = false;
        public bool fridgeDone = false;
        public bool counterDone = false;
        public bool potDone = false;
        public bool serveDone = false;


    // Important station-wide variables
    public int level = 1;
    public string recipeName;
    public int score = 0;


    public bool CanEnterStation()
    {
        // TODO:
        // Check if the previous step has been completed
        return true;
    }

    public void ExitStation(string stationLeaving)
    {
        // Mark station as done (false --> true)

        // Allow player movement
        player.GetComponent<PlayerMovement>().moveSpeed = 8f;

        // Show E-KeyFlag g.o. above next station

    }
}
