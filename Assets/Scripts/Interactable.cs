using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    // Tracks specific Interactable object player is using
    public static Interactable CurrentActive { get; private set; }

    private bool canInteract = false;
    private bool interacting = false;
    
    // Store loaded Scene struct (rather than just a string)
    private Scene loadedSubScene;

    private List<GameObject> mainSceneRoots = new List<GameObject>();

    [Header("Objects")]
    public GameObject station;
    public GameObject player;
    public string sceneToEnter;

    [Header("Interactions")]
    public InputAction enterAction;

    private void OnEnable() => enterAction.Enable();
    private void OnDisable() => enterAction.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) canInteract = true;
        Debug.Log("Player entered");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canInteract = false;
        Debug.Log("Player exited");
    }

    void Update()
    {
        if (enterAction.triggered && canInteract && !interacting)
        {
            interacting = true;
            CurrentActive = this; // Sets this instance as the active one
            LoadAndIsolateScene(sceneToEnter);
        }
    }

    public void LoadAndIsolateScene(string newSceneName)
    {
        Scene mainScene = SceneManager.GetActiveScene();
        mainSceneRoots.Clear();
        mainScene.GetRootGameObjects(mainSceneRoots);

        // Hide main scene objects
        foreach (GameObject root in mainSceneRoots)
        {
            root.SetActive(false);
        }

        // Load scene additively
        SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive).completed += (op) =>
        {
            // Cache the exact loaded scene instance
            loadedSubScene = SceneManager.GetSceneByName(newSceneName);
            
            if (loadedSubScene.IsValid())
            {
                SceneManager.SetActiveScene(loadedSubScene);
            }
            else
            {
                Debug.LogError($"Failed to find loaded scene named: '{newSceneName}'. Check Build Settings and spelling!");
            }

            Time.timeScale = 1f;
        };
    }

    public void ResumeMainScene(SubSceneResult returnedData = null)
    {
        // Check if stored scene reference is valid
        if (!loadedSubScene.IsValid() || !loadedSubScene.isLoaded)
        {
            Debug.LogError($"Cannot unload scene: '{loadedSubScene.name}' is invalid or not loaded.");
            return;
        }

        // Process returned data
        if (returnedData != null)
        {
            Debug.Log($"Data received by {gameObject.name}: recipe = {returnedData.recipe}");
            // TODO:
            // Handle returned data here !!!
        }

        // Unload using Scene struct
        SceneManager.UnloadSceneAsync(loadedSubScene).completed += (op) =>
        {
            // Reactivate main scene objects
            foreach (GameObject root in mainSceneRoots)
            {
                if (root != null) root.SetActive(true);
            }

            interacting = false;
            Time.timeScale = 1f;

            // Clear current active tracking
            if (CurrentActive == this)
            {
                CurrentActive = null;
            }
        };
    }
}

[System.Serializable]
public class SubSceneResult
{
    public int score;
    public bool isCompleted;
    public string message;
    public int recipe;
}