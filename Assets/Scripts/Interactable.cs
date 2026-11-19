
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private bool canInteract = false;
    private bool interacting = false;

    [Header("Objects")]
    public GameObject player;
    public GameObject stationPrefab;
    public GameObject stationCollider;
    public GameObject stationObject;

    [Header("References")]
    public LevelHandler levelHandler;

    [Header("Interactions")]
    public InputAction enterAction;

    private void OnEnable()
    {
        enterAction.Enable();
    }

    private void OnDisable()
    {
        enterAction.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canInteract = false;
    }

    public void ResetInteraction()
    {
        canInteract = false;
        interacting = false;
    }

    void Update()
    {
        if (enterAction.triggered && canInteract && !interacting)
        {
            // Check whether this is the correct station
            if (levelHandler == null)
            {
                Debug.LogError("LevelHandler reference missing");
                return;
            }

            if (!levelHandler.CanEnterStation(stationObject))
            {
                Debug.Log("This station is not available yet!");
                return;
            }

            // Allow interaction
            interacting = true;

            stationPrefab.SetActive(true);

            // Freeze player movement
            player.GetComponent<PlayerMovement>().moveSpeed = 0f;
        }
    }
}
